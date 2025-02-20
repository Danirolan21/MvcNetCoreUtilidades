using System;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace MvcNetCoreUtilidades.Helpers
{
    //VAMOS A OFRECER EN PROGRAMACION UNA ENUMERACION
    //CON LAS CARPETAS DE NUESTRO SERVIDOR
    public enum Folders { images, Falturas, Uploads, Temporal }
    public class HelperPathProvider
    {
        private IServer server;
        private IWebHostEnvironment hostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;

        public HelperPathProvider(IWebHostEnvironment hostEnvironment
            , IHttpContextAccessor httpContextAccessor, IServer server)
        {
            this.hostEnvironment = hostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.server = server;
        }

        public string MapPath(string fileName, Folders folder)
        {
            string carpeta = folder switch
            {
                Folders.images => "images",
                Folders.Falturas => "falturas",
                Folders.Uploads => "uploads",
                Folders.Temporal => "temporal",
                _ => throw new ArgumentOutOfRangeException(nameof(folder), folder, null)
            };

            return Path.Combine(this.hostEnvironment.WebRootPath, carpeta, fileName);
        }

        public string MapUrlPath(string fileName, Folders folder)
        {
            string carpeta = folder switch
            {
                Folders.images => "images",
                Folders.Falturas => "falturas",
                Folders.Uploads => "uploads",
                Folders.Temporal => "temporal",
                _ => throw new ArgumentOutOfRangeException(nameof(folder), folder, null)
            };
            var adresses =
                this.server.Features.Get<IServerAddressesFeature>().Addresses;
            string serverUrl = adresses.FirstOrDefault();
            var request = this.httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            return Path.Combine(serverUrl, carpeta, fileName);
        }
    }
}
