using System.Text.Json;
using System.Text.Json.Serialization;
using Akka.Actor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neo;
using Neo.Plugins;
using TencentCloudLogger;
using TencentCloudLogger.Http;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace TencentLogPlugin
{
    public class TencentLogger : Plugin
    {

        private TCloudClient _client;
        private TCloudLoggerChannel _channel;
        private TCloudLogger _logger;

        protected override void Configure()
        {
            var option = GetConfiguration().GetSection("TencentCloudLogger").Get<TCloudOption>();
            //var option = new TCloudOption()
            //{
            //    Level = LogLevel.Information,
            //    ApiHost = "http://ap-shanghai.cls.tencentcs.com",
            //    SecretId = "AKIDhDUVifNq2ye4E8lAJBm7C0Yqsd7KHxMU",
            //    SecretKey = "sVrwlLegRwA7mAI9i01lEQM1e17wUklW",
            //    Topic = "4807fc45-cce4-4062-9254-02867df09017",
            //};

            _client = new TCloudClient(option);
            _channel = new TCloudLoggerChannel(_client);
            _channel.Start();
            _logger = new TCloudLogger(_channel, option, "neo plugins logger");
            //Settings.Load(GetConfiguration());
            //string path = string.Format(Settings.Default.Path, Settings.Default.Network.ToString("X8"));
        }
        protected override void OnSystemLoaded(NeoSystem system)
        {
            Utility.Logging += Log;
        }


        public void Log(string source, Neo.LogLevel level, object message)
        {
            _logger?.Log(SwitchLevel(level), new EventId(0, source), message switch
            {
                string msg => msg,
                null => null,
                _ => message?.ToString(),
            });
        }


        private LogLevel SwitchLevel(Neo.LogLevel level)
        {
            return level switch
            {
                Neo.LogLevel.Debug => LogLevel.Debug,
                Neo.LogLevel.Error => LogLevel.Error,
                Neo.LogLevel.Fatal => LogLevel.Critical,
                Neo.LogLevel.Info => LogLevel.Information,
                Neo.LogLevel.Warning => LogLevel.Warning,
                _ => LogLevel.None
            };
        }
    }
}