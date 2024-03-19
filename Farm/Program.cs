using Microsoft.EntityFrameworkCore;
using Farm.Modelss;
using Farm.Service.IService;
using Farm.Services;
using Farm.Service;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddScoped<IOrderService, OrderService>();
		builder.Services.AddScoped<IProduceService, ProduceService>();
        builder.Services.AddScoped<IGardenService, GardenService>();
		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<IProcessService, ProcessService>();

		builder.Services.AddScoped<OrderService>();

		builder.Services.AddSession(options =>
		{
			options.IdleTimeout = TimeSpan.FromMinutes(30); 
			options.Cookie.HttpOnly = true; 
			options.Cookie.IsEssential = true; 
		});
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddDistributedMemoryCache();
		builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddCors(opts =>
        {
            opts.AddPolicy("CORSPolicy", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowCredentials().SetIsOriginAllowed((host) => true));
        });

        builder.Services.AddDbContext<FarmContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


		var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
		app.UseSession();
		app.UseHttpsRedirection();
        app.UseCors("CORSPolicy");
        app.UseAuthorization();
		app.MapControllers();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}
