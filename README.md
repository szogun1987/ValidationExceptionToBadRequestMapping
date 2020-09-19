# ValidationException to BadRequest mapping
ASP.Net Core extension that maps FluentValidations ValidationException occurrence to Bad Request http response.

Just call `UseValidationExceptionToBadRequestMapping` in your the Configure method of your Startup class.
```
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    ...
    app.UseValidationExceptionToBadRequestMapping(HttpStatusCode.BadRequest);
    app.UseMvc();
}
```