using Microsoft.AspNetCore.Authentication.Cookies;

namespace fues_prn212_chiennguyensrdn
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddRazorPages();

            // Add SignalR to the container.
            services.AddSignalR();

            // Add service to the container.
            //services.AddScoped<ICustomerRepository, CustomerRepository>();
            //services.AddScoped<IRoomInformationRepository, RoomInformationRepository>();
            //services.AddScoped<IBookingReservationRepository, BookingReservationRepository>();
            //services.AddScoped<IBookingDetailRepository, BookingDetailRepository>();

            // Add authentication services
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login"; // Đường dẫn tới trang đăng nhập
                    options.AccessDeniedPath = "/Account/AccessDenied";
                });

            // DI
            //services.AddDbContext<FuminiHotelManagementContext>(options => options.UseSqlServer(_configuration.GetConnectionString("SqlConnection")));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Use authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                //endpoints.MapHub<RoomHub>("/RoomHub");
            });
        }
    }
}
