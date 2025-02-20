﻿// Contrast Security, Inc licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using NLog;

namespace Contrast.K8s.AgentOperator.Core.Tls
{
    public interface IKestrelCertificateSelector : IDisposable
    {
        X509Certificate2? SelectCertificate(string? hostname);
        bool TakeOwnershipOfCertificate(TlsCertificateChain certificate);
        bool HasValidCertificate();
    }

    public class KestrelCertificateSelector : IKestrelCertificateSelector
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private TlsCertificateChain? _chain;
        //private IReadOnlyCollection<string>? _chainSans;

        public X509Certificate2? SelectCertificate(string? hostname)
        {
            if (_chain == null)
            {
                Logger.Warn($"A server certificate was requested for '{hostname ?? "<null>"}', but none was known at this time.");
            }

            //if (_chain != null
            //    && !string.IsNullOrWhiteSpace(hostname)
            //    && _chainSans != null
            //    && !_chainSans.Contains(hostname, StringComparer.OrdinalIgnoreCase))
            //{
            //    Logger.Warn($"A server certificate was requested for '{hostname}', but the currently loaded certificate doesn't match.");
            //}

            return _chain?.ServerCertificate;
        }

        public bool TakeOwnershipOfCertificate(TlsCertificateChain chain)
        {
            var ownershipTaken = chain.ServerCertificate.SerialNumber != _chain?.ServerCertificate.SerialNumber
                                 || chain.CaCertificate.SerialNumber != _chain?.CaCertificate.SerialNumber;

            if (ownershipTaken)
            {
                _chain?.Dispose();
                _chain = chain;
                //_chainSans = GetSans(_chain.ServerCertificate).ToList();

                //Logger.Trace($"Certificate has {_chainSans.Count} SAN's (SAN's: [{string.Join(", ", _chainSans)}]).");
            }

            return ownershipTaken;
        }

        public bool HasValidCertificate()
        {
            return _chain?.ServerCertificate != null;
        }

        private static IEnumerable<string> GetSans(X509Certificate2 certificate)
        {
            // This does not work under Linux - OpenSSL version?
            var scansLines = certificate.Extensions["2.5.29.17"]?.Format(true);
            if (scansLines != null)
            {
                var sans = scansLines.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var san in sans)
                {
                    var pair = san.Split("=", 2, StringSplitOptions.RemoveEmptyEntries);
                    if (pair.Length == 2)
                    {
                        yield return pair[1].Trim();
                    }
                }
            }
        }

        public void Dispose()
        {
            _chain?.Dispose();
        }
    }
}
