using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Helpers;

namespace MvcNetCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
        private HelperPathProvider helperPath; 

        public UploadFilesController(HelperPathProvider helperPath)
        {
            this.helperPath = helperPath;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SubirFichero()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> 
            SubirFichero(IFormFile fichero)
        {
            string fileName = fichero.FileName;
            //LAS RUTAS DE FICHEROS NO DEBO ESCRIBIRLAS, TENGO QUE GENERAR
            //DICHAS RUTAS CON EL SISTEMA DONDE ESTOY TRABAJANDO

            string path =
                this.helperPath.MapPath(fileName, Folders.images);
            //PARA SUBIR EL FICHERO SE UTILIZA Stream con IFormFile
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            ViewData["PATH"] = helperPath.MapUrlPath(fileName, Folders.images);
            return View();
        }
    }
}
