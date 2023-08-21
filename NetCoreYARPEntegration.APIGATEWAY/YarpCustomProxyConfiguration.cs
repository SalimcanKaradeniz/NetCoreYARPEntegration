using Microsoft.Extensions.Primitives;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.LoadBalancing;

namespace NetCoreYARPEntegration.APIGATEWAY
{
    public class YarpCustomProxyConfiguration : IProxyConfigProvider
    {
        private CustomMemoryConfig _config;

        public YarpCustomProxyConfiguration()
        {
            var routeConfigs = new[]
            {
                new RouteConfig
                {
                    RouteId = "order_routes",
                    ClusterId = "order_api",
                    Match = new RouteMatch
                    {
                        Path = "/api/orders/{**catch-all}"
                    }
                },
                new RouteConfig
                {
                    RouteId = "product_routes",
                    ClusterId = "product_api",
                    Match = new RouteMatch
                    {
                        Path = "/api/products/{**catch-all}"
                    }
                },
                new RouteConfig
                {
                    RouteId = "shipping_routes",
                    ClusterId = "shipping_api",
                    Match = new RouteMatch
                    {
                        Path = "/api/shippings/{**catch-all}"
                    }
                }
            };

            var clusterConfigs = new[]
            {
                new ClusterConfig
                {
                    ClusterId = "order_api",
                    LoadBalancingPolicy = LoadBalancingPolicies.RoundRobin,
                    Destinations = new Dictionary<string, DestinationConfig>
                    {
                        { "destination", new DestinationConfig { Address = "https://localhost:7297/" } },
                    }
                },
                new ClusterConfig
                {
                    ClusterId = "product_api",
                    LoadBalancingPolicy = LoadBalancingPolicies.RoundRobin,
                    Destinations = new Dictionary<string, DestinationConfig>
                    {
                        { "destination", new DestinationConfig { Address = "https://localhost:7158/" } },
                    }
                },
                new ClusterConfig
                {
                    ClusterId = "shipping_api",
                    LoadBalancingPolicy = LoadBalancingPolicies.RoundRobin,
                    Destinations = new Dictionary<string, DestinationConfig>
                    {
                        { "destination", new DestinationConfig { Address = "https://localhost:7285/" } },
                    }
                },

            };

            _config = new CustomMemoryConfig(routeConfigs, clusterConfigs);

        }
        public IProxyConfig GetConfig() => _config;

        private class CustomMemoryConfig : IProxyConfig
        {
            private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            public CustomMemoryConfig(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
            {
                Routes = routes;
                Clusters = clusters;
                ChangeToken = new CancellationChangeToken(cancellationTokenSource.Token);
            }
            public IReadOnlyList<RouteConfig> Routes { get; }

            public IReadOnlyList<ClusterConfig> Clusters { get; }

            public IChangeToken ChangeToken { get; }

            internal void SignalChange()
            {
                cancellationTokenSource.Cancel();
            }
        }
    }
}
