using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace SteamTrade
{
    /// <summary>
    /// Digs through HTTP redirects until a non-redirected URL is found.
    /// </summary>
    public class Digger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Digger"/> class.
        /// </summary>
        public Digger()
            : this(20)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Digger"/> class.
        /// </summary>
        /// <param name="maximumDepth">The maximum depth of redirects to parse.</param>
        public Digger(int maximumDepth)
        {
            this.MaximumDepth = maximumDepth;
        }

        /// <summary>
        /// Gets the maximum depth of redirects to parse.
        /// </summary>
        /// <value>The maximum depth of redirects to parse.</value>
        public int MaximumDepth
        {
            get;
            private set;
        }

        /// <summary>
        /// Resolves any redirects at the specified URI.
        /// </summary>
        /// <param name="destination">The initial URI.</param>
        /// <returns>The URI after resolving any HTTP redirects.</returns>
        public Uri Resolve(Uri destination)
        {
            List<Uri> redirectHistory = new List<Uri>();
            return this.Resolve(destination, redirectHistory);
        }

        /// <summary>
        /// Resolves any redirects at the specified URI.
        /// </summary>
        /// <param name="destination">The initial URI.</param>
        /// <param name="redirectHistory">A collection of <see cref="Uri"/> objects representing the redirect history.</param>
        /// <returns>The URI after resolving any HTTP redirects.</returns>
        public Uri Resolve(Uri destination, ICollection<Uri> redirectHistory)
        {
            redirectHistory.Add(destination);
            return this.Resolve(destination, this.MaximumDepth, redirectHistory);
        }

        /// <summary>
        /// Resolves any redirects at the specified URI.
        /// </summary>
        /// <param name="destination">The initial URI.</param>
        /// <param name="hopsLeft">The maximum number of redirects left to follow.</param>
        /// <param name="redirectHistory">A collection of <see cref="Uri"/> objects representing the redirect history.</param>
        /// <returns>The URI after resolving any HTTP redirects.</returns>
        private Uri Resolve(Uri destination, int hopsLeft, ICollection<Uri> redirectHistory)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destination);
            request.AllowAutoRedirect = false;
            request.Method = "HEAD";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Uri resolvedUri;

            if (response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.Moved ||
                response.StatusCode == HttpStatusCode.MovedPermanently)
            {
                if (hopsLeft > 0)
                {
                    Uri redirectUri = new Uri(response.GetResponseHeader("Location"));
                    if (redirectHistory.Contains(redirectUri))
                    {
                        throw new Exception("Recursive redirection found");
                    }

                    redirectHistory.Add(redirectUri);
                    resolvedUri = this.Resolve(redirectUri, hopsLeft - 1, redirectHistory);
                }
                else
                {
                    throw new Exception("Maximum redirect depth reached");
                }
            }
            else
            {
                resolvedUri = response.ResponseUri;
            }

            return resolvedUri;
        }
    }

    public class MyWebClient : WebClient
    {
        Uri _responseUri;

        public Uri ResponseUri
        {
            get { return _responseUri; }
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            _responseUri = response.ResponseUri;
            return response;
        }
    }
}
