using AutoWrapper;

namespace API.Utils;

public static class ApplicationConfig
{
    public static WebApplication UseAutoWrapper(this WebApplication app)
    {
        app.UseApiResponseAndExceptionWrapper(
            new AutoWrapperOptions
            {
                IsApiOnly = false,
                ShowIsErrorFlagForSuccessfulResponse = true,
                WrapWhenApiPathStartsWith = "/v1/bcs"
            }
        );
        return app;
    }
}
