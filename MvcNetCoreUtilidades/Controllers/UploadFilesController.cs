using Microsoft.AspNetCore.Mvc;

namespace MvcNetCoreUtilidades.Controllers
{
    public class UploadFilesController : Controller
    {
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
            //VAMOS A COMENZAR ALMACENANDO EL FICHERO EN LOS
            //ELEMENTOS TEMPORALES
            string tempFolder = Path.GetTempPath();
            string fileName = fichero.FileName;
            //CUANDO HABLAMOS DE FICHEROS Y DE RUTAS DE SISTEMA
            //ESTAMOS PENSANDO EN LO SIGUIENTE
            //C:\miruta\carpeta\file.txt
            //TENEMOS QUE TENER EN CUENTA QUE ESTAMOS DENTRO DE NET CORE
            //PODEMOS MONTAR EL SERVIDOR DONDE DESEEMOS
            //C:/miruta/carpeta/file.txt
            //..miruta\carpeta\file.txt
            //LAS RUTAS DE FICHEROS NO DEBO ESCRIBIRLAS, TENGO QUE GENERAR
            //DICHAS RUTAS CON EL SISTEMA DONDE ESTOY TRABAJANDO
            string path = Path.Combine(tempFolder, fileName);
            //PARA SUBIR EL FICHERO SE UTILIZA Stream con IFormFile
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            ViewData["MENSAJE"] = "Fichero subido a " + path;
            return View();
        }
    }
}
