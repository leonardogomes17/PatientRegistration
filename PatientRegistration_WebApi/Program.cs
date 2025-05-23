using PatientRegistration.DI;
using PatientRegistrations.Domain.Interfaces;
using StoreOfBuild.Web.Filters;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  // Allow requests from any origin (not recommended for production)
               .AllowAnyMethod()  // Allow all HTTP methods
               .AllowAnyHeader()  // Allow all headers
               .AllowCredentials(); // Allow credentials (cookies, HTTP authentication)
    });

    // Especify Url
    //options.AddPolicy("MySpecificPolicy", builder =>
    //{
    //    builder.WithOrigins("http://localhost:4200") // Allow requests from specific origins
    //           .WithMethods("GET", "POST", "PUT", "DELETE") // Allow specific HTTP methods
    //           .WithHeaders("Content-Type", "Authorization"); // Allow specific headers
    //});
});

Bootstrap.Configure(builder.Services, builder.Configuration.GetConnectionString("PatientRegistrationDbContext"));

builder.Services.AddMvc(config =>
{
    config.Filters.Add(typeof(CustomExceptionFilter));
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Patient}/{action=GetById}/{id?}")
    .WithStaticAssets();

app.Use(async (context, next) =>
{
  //Request
  await next.Invoke();
  //Response
  var UnitOfWork = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork));
  await UnitOfWork.Save();
});

app.Run();
