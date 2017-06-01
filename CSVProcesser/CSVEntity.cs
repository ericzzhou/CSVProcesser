using CSVProcesser.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSVProcesser
{
    [Serializable]
    public class CSVEntity
    {
        [DataMapping("x-record-type", TypeCode.String)]
        public string RecordType { get; set; }

        [DataMapping("x-page-id", TypeCode.String)]
        public string PageId { get; set; }

        [DataMapping("x-session-id", TypeCode.String)]
        public string SessionId { get; set; }

        [DataMapping("x-server-id", TypeCode.String)]
        public string ServerId { get; set; }

        [DataMapping("x-page-name", TypeCode.String)]
        public string PageName { get; set; }

        [DataMapping("cs(Host)", TypeCode.String)]
        public string CSHost { get; set; }

        [DataMapping("cs-uri-stem", TypeCode.String)]
        public string CSUriStem { get; set; }

        [DataMapping("cs-uri-query", TypeCode.String)]
        public string CSUriQuery { get; set; }

        [DataMapping("cs(Referrer)", TypeCode.String)]
        public string CSReferrer { get; set; }

        [DataMapping("x-redirect-host", TypeCode.String)]
        public string RedirectHost { get; set; }

        [DataMapping("x-redirect-url", TypeCode.String)]
        public string RedirectUrl { get; set; }

        [DataMapping("cs(Cookie)", TypeCode.String)]
        public string CSCookie { get; set; }

        [DataMapping("sc(Set-Cookie)", TypeCode.String)]
        public string CSSetCookie { get; set; }

        [DataMapping("x-cs-post", TypeCode.String)]
        public string CSPost { get; set; }

        [DataMapping("x-start-time", TypeCode.String)]
        public string StartTime { get; set; }

        [DataMapping("x-start-time-utc", TypeCode.String)]
        public string StartTimeUtc { get; set; }

        [DataMapping("x-end-time", TypeCode.String)]
        public string EndTime { get; set; }

        [DataMapping("x-end-time-utc", TypeCode.String)]
        public string EndTimeUtc { get; set; }

        [DataMapping("c-ip", TypeCode.String)]
        public string CIP { get; set; }

        [DataMapping("c-port", TypeCode.String)]
        public string CPort { get; set; }

        [DataMapping("x-first-public-ip", TypeCode.String)]
        public string FirstPublicIP { get; set; }

        [DataMapping("s-ip", TypeCode.String)]
        public string SIP { get; set; }

        [DataMapping("s-port", TypeCode.String)]
        public string SPort { get; set; }

        [DataMapping("sc-bytes", TypeCode.String)]
        public string SCBytes { get; set; }

        [DataMapping("x-throughput", TypeCode.String)]
        public string Throughput { get; set; }

        [DataMapping("x-tcp-packet-count", TypeCode.String)]
        public string TcpPacketCount { get; set; }

        [DataMapping("x-tcp-rtt-count", TypeCode.String)]
        public string TcpRttCount { get; set; }

        [DataMapping("x-tcp-rtt", TypeCode.String)]
        public string TcpRtt { get; set; }

        [DataMapping("x-tcp-ooo", TypeCode.String)]
        public string TcpOOO { get; set; }

        [DataMapping("x-tcp-retrx", TypeCode.String)]
        public string TcpRetrx { get; set; }

        [DataMapping("x-redirect-time", TypeCode.String)]
        public string RedirectTime { get; set; }

        [DataMapping("x-redirect-ssl-time", TypeCode.String)]
        public string RedirectSslTime { get; set; }

        [DataMapping("x-redirect-ssl-count", TypeCode.String)]
        public string RedirectSslCount { get; set; }

        [DataMapping("x-redirect-process-time", TypeCode.String)]
        public string RedirectProcessTime { get; set; }

        [DataMapping("x-redirect-network-time", TypeCode.String)]
        public string RedirectNetworkTime { get; set; }

        [DataMapping("x-redirect-count", TypeCode.String)]
        public string RedirectCount { get; set; }

        [DataMapping("x-ssl-time", TypeCode.String)]
        public string SslTime { get; set; }

        [DataMapping("x-ssl-count", TypeCode.String)]
        public string SslCount { get; set; }

        [DataMapping("x-process-time", TypeCode.String)]
        public string ProcessTime { get; set; }

        [DataMapping("x-network-time", TypeCode.String)]
        public string NetworkTime { get; set; }

        [DataMapping("x-idle-time", TypeCode.String)]
        public string IdleTime { get; set; }

        [DataMapping("x-e2e-time", TypeCode.String)]
        public string E2eTime { get; set; }

        [DataMapping("cs-method", TypeCode.String)]
        public string Method { get; set; }

        [DataMapping("cs-version", TypeCode.String)]
        public string Version { get; set; }

        [DataMapping("x-sc-mimetype", TypeCode.String)]
        public string ScMimetype { get; set; }

        [DataMapping("sc-status", TypeCode.String)]
        public string Status { get; set; }

        [DataMapping("x-document", TypeCode.String)]
        public string Document { get; set; }

        [DataMapping("x-container", TypeCode.String)]
        public string Container { get; set; }

        [DataMapping("x-aborted", TypeCode.String)]
        public string Aborted { get; set; }

        [DataMapping("x-secure", TypeCode.String)]
        public string Secure { get; set; }

        [DataMapping("x-secure-count", TypeCode.String)]
        public string SecureCount { get; set; }

        [DataMapping("x-content-count", TypeCode.String)]
        public string ContentCount { get; set; }

        [DataMapping("x-errored-count", TypeCode.String)]
        public string ErroredCount { get; set; }

        [DataMapping("x-slt-broken", TypeCode.String)]
        public string SltBroken { get; set; }

        [DataMapping("x-nw-error-count", TypeCode.String)]
        public string NwErrorCount { get; set; }

        [DataMapping("x-cl-error-count", TypeCode.String)]
        public string ClErrorCount { get; set; }

        [DataMapping("x-sv-error-count", TypeCode.String)]
        public string SvErrorCount { get; set; }

        [DataMapping("x-ap-error-count", TypeCode.String)]
        public string ApErrorCount { get; set; }

        [DataMapping("x-ct-error-count", TypeCode.String)]
        public string CtErrorCount { get; set; }

        [DataMapping("x-cu-error-count", TypeCode.String)]
        public string CuErrorCount { get; set; }

        [DataMapping("x-nw-info-count", TypeCode.String)]
        public string NwInfoCount { get; set; }

        [DataMapping("x-cl-info-count", TypeCode.String)]
        public string ClInfoCount { get; set; }

        [DataMapping("x-sv-info-count", TypeCode.String)]
        public string SvInfoCount { get; set; }

        [DataMapping("x-ap-info-count", TypeCode.String)]
        public string ApInfoCount { get; set; }

        [DataMapping("x-ct-info-count", TypeCode.String)]
        public string CtInfoCount { get; set; }

        [DataMapping("x-cu-info-count", TypeCode.String)]
        public string CuInfoCount { get; set; }

        [DataMapping("x-errors", TypeCode.String)]
        public string Errors { get; set; }

        [DataMapping("x-info", TypeCode.String)]
        public string Info { get; set; }

        [DataMapping("x-wp", TypeCode.String)]
        public string WP { get; set; }

        [DataMapping("cs(User-Agent)", TypeCode.String)]
        public string UserAgent { get; set; }

        [DataMapping("x-first-public-geo-country", TypeCode.String)]
        public string FirstPublicGeoCountry { get; set; }

        [DataMapping("x-first-public-geo-country-string", TypeCode.String)]
        public string FirstPublicGeoCountryString { get; set; }

        [DataMapping("x-first-public-geo-region", TypeCode.String)]
        public string FirstPublicGeoRegion { get; set; }

        [DataMapping("x-first-public-geo-region-string", TypeCode.String)]
        public string FirstPublicGeoRegionString { get; set; }

        [DataMapping("x-first-public-geo-city", TypeCode.String)]
        public string FirstPublicGeoCity { get; set; }

        [DataMapping("x-first-public-geo-metro-area", TypeCode.String)]
        public string FirstPublicGeoMetroArea { get; set; }

        [DataMapping("x-first-public-geo-organization", TypeCode.String)]
        public string FirstPublicGeoOrganization { get; set; }

        [DataMapping("x-first-public-geo-isp", TypeCode.String)]
        public string FirstPublicGeoIsp { get; set; }

        [DataMapping("x-first-public-geo-dns-name", TypeCode.String)]
        public string FirstPublicGeoDnsName { get; set; }

        [DataMapping("x-first-public-geo-latitude", TypeCode.String)]
        public string FirstPublicGeoLatitude { get; set; }

        [DataMapping("x-first-public-geo-longitude", TypeCode.String)]
        public string FirstPublicGeoLongitude { get; set; }

        [DataMapping("x-page-render-time", TypeCode.String)]
        public string PageRenderTime { get; set; }

        [DataMapping("x-prm-instrumented", TypeCode.String)]
        public string PrmInstrumented { get; set; }

        [DataMapping("x-aborted-count", TypeCode.String)]
        public string AbortedCount { get; set; }

        [DataMapping("x-errored-aborted-count", TypeCode.String)]
        public string ErroredAbortedCount { get; set; }

        [DataMapping("x-akamai-download-time-sum", TypeCode.String)]
        public string AkamaiDownloadTimeSum { get; set; }

        [DataMapping("x-delivery-mode", TypeCode.String)]
        public string DeliveryMode { get; set; }

        [DataMapping("x-object-delivery-origin-count", TypeCode.String)]
        public string ObjectDeliveryOriginCount { get; set; }

        [DataMapping("x-object-delivery-akacachemgt-count", TypeCode.String)]
        public string ObjectDeliveryAkacachemgtCount { get; set; }

        [DataMapping("x-object-delivery-akacachehit-count", TypeCode.String)]
        public string ObjectDeliveryAkacachehitCount { get; set; }

        [DataMapping("x-object-delivery-akacachemiss-count", TypeCode.String)]
        public string ObjectDeliveryAkacachemissCount { get; set; }

        [DataMapping("collectorFeedNames", TypeCode.String)]
        public string CollectorFeedNames { get; set; }

        [DataMapping("browser", TypeCode.String)]
        public string Browser { get; set; }

        [DataMapping("os", TypeCode.String)]
        public string OS { get; set; }

        [DataMapping("trace", TypeCode.String)]
        public string Trace { get; set; }

        [DataMapping("x-application-name", TypeCode.String)]
        public string ApplicationName { get; set; }

        [DataMapping("x-custom-continent", TypeCode.String)]
        public string CustomContinent { get; set; }

        [DataMapping("x-custom-referrer-name", TypeCode.String)]
        public string CustomReferrerName { get; set; }
    }
}
