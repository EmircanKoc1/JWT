using JWT2.Services.Abstracts;
using JWT2.Services.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;


services.AddControllers();
//services.AddEndpointsApiExplorer();
//services.AddSwaggerGen();

services.AddTransient<ITokenService, TokenService>();



services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"])),
            ValidAudience = builder.Configuration["jwt:audience"],
            ValidIssuer = builder.Configuration["jwt:issuer"],



        };


    });

services.AddAuthorization();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

//app.UseHttpsRedirection();


//app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
//app.MapDefaultControllerRoute();

app.Run();
