using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SourceVietNam_Version02.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        //Khai báo 1 Property 
        public string chuoi { get; set; }
        public void OnGet()
        {
            //Thiết lập thông tin chuỗi 
            chuoi = "Model - Sử dụng Model trong OnGet()";
        }
    }
}