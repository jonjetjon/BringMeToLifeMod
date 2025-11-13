using System.Text.Json;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Eft.Common;
using SPTarkov.Server.Core.Utils;
using RevivalModServer.Models;

namespace RevivalModServer;

[Injectable]
public class CustomStaticRouter : StaticRouter
{
    private static ModConfig? _modConfig;

    public CustomStaticRouter(
        JsonUtil jsonUtil) : base(
        jsonUtil,
        GetCustomRoutes()
    )
    {}

    public void PassConfig(ModConfig config)
    {
        _modConfig = config;
    }

    private static List<RouteAction> GetCustomRoutes()
    {
        return
        [
            new RouteAction<EmptyRequestData>(
                "/bringmetolife/load",
                async (
                    url,
                    info,
                    sessionId,
                    output
                ) => await HandleRoute(url, info, sessionId)
            )
        ];
    }

    private static ValueTask<string> HandleRoute(string url, EmptyRequestData info, MongoId sessionId)
    {
        return new ValueTask<string>(JsonSerializer.Serialize(_modConfig));
    }
}