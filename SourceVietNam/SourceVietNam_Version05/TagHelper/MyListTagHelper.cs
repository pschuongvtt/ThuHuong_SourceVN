using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SourceVietNam_Version05
{
    //Khai báo để gọi sử dụng thẻ riêng "mylist"
    [HtmlTargetElement("mylist")]
    public class MyListTagHelper : TagHelper 
    {
            //Thuộc tính sẽ là list-title: Thiết lập tiêu đề 
            public string ListTitle { get; set; }

            //Thuộc tính sẽ là list-items: Thiết lập danh sách tên 
            public List<string> ListItems {set; get;}
            
            //Để xử lý sinh ra code html trong TagHelper riêng, ta dùng override phương thức nạp chồng phương thức tên "Process" với những tham số tương ứng
            public override void Process(TagHelperContext context, TagHelperOutput output)
            {
                  output.TagName = "ul";    //Sinh ra phần tử Html <ul> 
                  output.TagMode = TagMode.StartTagAndEndTag; //Sinh ra có đóng thẻ <ul/>

                  output.Attributes.SetAttribute("class", "list-group"); //Sinh ra lớp css cho thẻ ul bằng cách truy cập set Attributes
                  output.PreElement.AppendHtml($"<h2>{ListTitle}</h2>"); //Chèm vào phía trước thẻ <ul></ul> là 1 òng có thẻ <h2>TieuDe</h2>


                  StringBuilder content = new StringBuilder();
                  foreach (var item in ListItems)
                  {
                      content.Append($@"<li class=""list-group-item"">{item}</li>"); //Bên trong thẻ <ul> là các phần <li>, sau đó duyệt qua danh sách tên các phần tử, add vào thẻ <li/>
                  }
                  output.Content.SetHtmlContent(content.ToString()); //Toàn bộ nội dung được thiết lập này là Content của <ul/>
            }

    }
}