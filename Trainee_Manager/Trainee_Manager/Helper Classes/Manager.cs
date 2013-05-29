// There's one main class: OAuth.Manager.  It handles interaction with the OAuth-
// enabled service, for requesting temporary tokens (aka request tokens), as well
// as access tokens. It also provides a convenient way to construct an oauth
// Authorization header for use in any Http transaction.
//
//
// -------------------------------------------------------
// Dino Chiesa
// Tue, 14 Dec 2010  12:31
//


namespace Helper_Classes.Trainee_Manager
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Security.Cryptography;

    using CropperPlugins.Utils;
    using System.Net;
    using System.IO;
    using System.IO.Compression;

    public class Manager
    {
        public Manager()
        {
            _random = new Random();
            _params = new Dictionary<String, String>();
            _params["callback"] = "oob"; // presume "desktop" consumer
            _params["consumer_key"] = "90d3acdc4b9d281fd376a895328beee921efc31c";
            _params["consumer_secret"] = "20ee6bbb5f829ae2b101caac09d887312c126499";
            _params["timestamp"] = GenerateTimeStamp();
            _params["nonce"] = GenerateNonce();
            _params["signature_method"] = "HMAC-SHA1";
            _params["signature"] = "";
            _params["token"] = "";
            _params["token_secret"] = "";
            _params["version"] = "1.0";
        }


        public Manager(string consumerKey,
                       string consumerSecret,
                       string token,
                       string tokenSecret)
            : this()
        {
            _params["consumer_key"] = consumerKey;
            _params["consumer_secret"] = consumerSecret;
            _params["token"] = token;
            _params["token_secret"] = tokenSecret;
        }


        public string this[string ix]
        {
            get
            {
                if (_params.ContainsKey(ix))
                    return _params[ix];
                throw new ArgumentException(ix);
            }
            set
            {
                if (!_params.ContainsKey(ix))
                    throw new ArgumentException(ix);
                _params[ix] = value;
            }
        }



        private string GenerateTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - _epoch;
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }


        private void NewRequest()
        {
            _params["nonce"] = GenerateNonce();
            _params["timestamp"] = GenerateTimeStamp();
        }


        private string GenerateNonce()
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                int g = _random.Next(3);
                switch (g)
                {
                    case 0:
                        // lowercase alpha
                        sb.Append((char)(_random.Next(26) + 97), 1);
                        break;
                    default:
                        // numeric digits
                        sb.Append((char)(_random.Next(10) + 48), 1);
                        break;
                }
            }
            return sb.ToString();
        }


        private Dictionary<String, String> ExtractQueryParameters(string queryString)
        {
            if (queryString.StartsWith("?"))
                queryString = queryString.Remove(0, 1);

            var result = new Dictionary<String, String>();

            if (string.IsNullOrEmpty(queryString))
                return result;

            foreach (string s in queryString.Split('&'))
            {
                if (!string.IsNullOrEmpty(s) && !s.StartsWith("oauth_"))
                {
                    if (s.IndexOf('=') > -1)
                    {
                        string[] temp = s.Split('=');
                        result.Add(temp[0], temp[1]);
                    }
                    else
                        result.Add(s, string.Empty);
                }
            }

            return result;
        }



        public static string UrlEncode(string value)
        {
            var result = new System.Text.StringBuilder();
            foreach (char symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                    result.Append(symbol);
                else
                    result.Append('%' + String.Format("{0:X2}", (int)symbol));
            }
            return result.ToString();
        }
        private static string unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";


        private static string EncodeRequestParameters(ICollection<KeyValuePair<String, String>> p)
        {
            var sb = new System.Text.StringBuilder();
            foreach (KeyValuePair<String, String> item in p.OrderBy(x => x.Key))
            {
                if (!String.IsNullOrEmpty(item.Value) &&
                    !item.Key.EndsWith("secret"))
                    sb.AppendFormat("oauth_{0}=\"{1}\", ",
                                    item.Key,
                                    UrlEncode(item.Value));
            }

            return sb.ToString().TrimEnd(' ').TrimEnd(',');
        }

        public OAuthResponse AcquireRequestToken(string uri, string method)
        {
            NewRequest();
            var authzHeader = GetAuthorizationHeader(uri, method);

            // prepare the token request
            var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
            request.Headers.Add("Authorization", authzHeader);
            request.Method = method;

            using (var response = (System.Net.HttpWebResponse)request.GetResponse())
            {
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    var r = new OAuthResponse(reader.ReadToEnd());
                    this["token"] = r["oauth_token"];

                    // Sometimes the request_token URL gives us an access token,
                    // with no user interaction required. Eg, when prior approval
                    // has already been granted.
                    try
                    {
                        if (r["oauth_token_secret"] != null)
                            this["token_secret"] = r["oauth_token_secret"];
                    }
                    catch { }
                    return r;
                }
            }
        }


        public OAuthResponse AcquireAccessToken(string uri, string method, string pin)
        {
            NewRequest();
            _params["verifier"] = pin;

            var authzHeader = GetAuthorizationHeader(uri, method);

            // prepare the token request
            var request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
            request.Headers.Add("Authorization", authzHeader);
            request.Method = method;

            using (var response = (System.Net.HttpWebResponse)request.GetResponse())
            {
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    var r = new OAuthResponse(reader.ReadToEnd());
                    this["token"] = r["oauth_token"];
                    this["token_secret"] = r["oauth_token_secret"];
                    return r;
                }
            }
        }



        public string GenerateCredsHeader(string uri, string method, string realm)
        {
            NewRequest();
            var authzHeader = GetAuthorizationHeader(uri, method, realm);
            return authzHeader;
        }


        public string GenerateAuthzHeader(string uri, string method)
        {
            NewRequest();
            var authzHeader = GetAuthorizationHeader(uri, method, null);
            return authzHeader;
        }

        private string GetAuthorizationHeader(string uri, string method)
        {
            return GetAuthorizationHeader(uri, method, null);
        }

        private string GetAuthorizationHeader(string uri, string method, string realm)
        {
            if (string.IsNullOrEmpty(this._params["consumer_key"]))
                throw new ArgumentNullException("consumer_key");

            if (string.IsNullOrEmpty(this._params["signature_method"]))
                throw new ArgumentNullException("signature_method");

            Sign(uri, method);

            var erp = EncodeRequestParameters(this._params);
            Tracing.Trace("erp = {0}", erp);
            return (String.IsNullOrEmpty(realm))
                ? "OAuth " + erp
                : String.Format("OAuth realm=\"{0}\", ", realm) + erp;
        }


        private void Sign(string uri, string method)
        {
            var signatureBase = GetSignatureBase(uri, method);
            var hash = GetHash();

            byte[] dataBuffer = System.Text.Encoding.ASCII.GetBytes(signatureBase);
            byte[] hashBytes = hash.ComputeHash(dataBuffer);

            this["signature"] = Convert.ToBase64String(hashBytes);
        }

        private string GetSignatureBase(string url, string method)
        {
            // normalize the URI
            var uri = new Uri(url);
            var normUrl = string.Format("{0}://{1}", uri.Scheme, uri.Host);
            if (!((uri.Scheme == "http" && uri.Port == 80) ||
                  (uri.Scheme == "https" && uri.Port == 443)))
                normUrl += ":" + uri.Port;

            normUrl += uri.AbsolutePath;

            // the sigbase starts with the method and the encoded URI
            var sb = new System.Text.StringBuilder();
            sb.Append(method)
                .Append('&')
                .Append(UrlEncode(normUrl))
                .Append('&');

            // the parameters follow - all oauth params plus any params on
            // the uri
            // each uri may have a distinct set of query params
            var p = ExtractQueryParameters(uri.Query);
            // add all non-empty params to the "current" params
            foreach (var p1 in this._params)
            {
                // Exclude all oauth params that are secret or
                // signatures; any secrets should be kept to ourselves,
                // and any existing signature will be invalid.
                if (!String.IsNullOrEmpty(this._params[p1.Key]) &&
                    !p1.Key.EndsWith("_secret") &&
                    !p1.Key.EndsWith("signature"))
                    p.Add("oauth_" + p1.Key, p1.Value);
            }

            // concat+format all those params
            var sb1 = new System.Text.StringBuilder();
            foreach (KeyValuePair<String, String> item in p.OrderBy(x => x.Key))
            {
                // even "empty" params need to be encoded this way.
                sb1.AppendFormat("{0}={1}&", item.Key, item.Value);
            }

            // append the UrlEncoded version of that string to the sigbase
            sb.Append(UrlEncode(sb1.ToString().TrimEnd('&')));
            var result = sb.ToString();
            Tracing.Trace("Sigbase: '{0}'", result);
            return result;
        }



        private HashAlgorithm GetHash()
        {
            if (this["signature_method"] != "HMAC-SHA1")
                throw new NotImplementedException();

            string keystring = string.Format("{0}&{1}",
                                             UrlEncode(this["consumer_secret"]),
                                             UrlEncode(this["token_secret"]));
            Tracing.Trace("keystring: '{0}'", keystring);
            var hmacsha1 = new HMACSHA1
            {
                Key = System.Text.Encoding.ASCII.GetBytes(keystring)
            };
            return hmacsha1;
        }

#if BROKEN
        /// <summary>
        ///   Return the oauth string that can be used in an Authorization
        ///   header. All the oauth terms appear in the string, in alphabetical
        ///   order.
        /// </summary>
        public string GetOAuthHeader()
        {
            return EncodeRequestParameters(this._params);
        }
#endif
        private static readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        private Dictionary<String, String> _params;
        private Random _random;
    }


    /// <summary>
    ///   A class to hold an OAuth response message.
    /// </summary>
    public class OAuthResponse
    {
        /// <summary>
        ///   All of the text in the response. This is useful if the app wants
        ///   to do its own parsing.
        /// </summary>
        public string AllText { get; set; }
        private Dictionary<String, String> _params;

        /// <summary>
        ///   a Dictionary of response parameters.
        /// </summary>
        public string this[string ix]
        {
            get
            {
                return _params[ix];
            }
        }


        public OAuthResponse(string alltext)
        {
            AllText = alltext;
            _params = new Dictionary<String, String>();
            var kvpairs = alltext.Split('&');
            foreach (var pair in kvpairs)
            {
                var kv = pair.Split('=');
                _params.Add(kv[0], kv[1]);
            }
            // expected keys:
            //   oauth_token, oauth_token_secret, user_id, screen_name, etc
        }
    }
}
