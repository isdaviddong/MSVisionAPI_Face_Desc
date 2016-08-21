using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.ProjectOxford.Vision;

namespace testVisionAPI
{
    public partial class _default : System.Web.UI.Page
    {
        //TODO : 換成你的key
        const string VisionAPIKey = "換成你的key";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            //如果沒有上傳就掰掰
            if (!this.FileUpload1.HasFile) return;

            //先把用戶上傳的檔案存起來
            string filename = Guid.NewGuid() + System.IO.Path.GetExtension(this.FileUpload1.PostedFile.FileName);
            string filePath = MapPath("pic/" + filename);
            //存檔
            this.FileUpload1.PostedFile.SaveAs(filePath);
            //顯示圖檔
            this.Image1.ImageUrl = "pic/" + filename;

            //將圖檔載入Bitmap ，便於繪圖，並且直接產生 Graphics物件
            var fs1 = new FileStream(filePath, FileMode.Open);
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(fs1);
            Graphics g = Graphics.FromImage(bmp);
            fs1.Close();

            //使用Computer Vision API
            var visionClient = new Microsoft.ProjectOxford.Vision.VisionServiceClient(VisionAPIKey);

            //分析圖片(從FileUpload1.PostedFile.InputStream取得影像)
            //分析 Faces & Description 
            var Results = await visionClient.AnalyzeImageAsync(this.FileUpload1.PostedFile.InputStream,
                new VisualFeature[] { VisualFeature.Faces, VisualFeature.Description });

            int isM = 0, isF = 0;
            //列出每一個找到的臉
            foreach (var Face in Results.Faces)
            {
                //取得人臉位置
                var faceRect = Face.FaceRectangle;
                //繪製人臉紅框 
                g.DrawRectangle(
                            new Pen(Brushes.Red, 10),
                            new Rectangle(faceRect.Left, faceRect.Top,
                                faceRect.Width, faceRect.Height));
                //計算幾男幾女
                if (Face.Gender.StartsWith("M"))
                    isM += 1;
                else
                    isF += 1;
            }

            //顯示圖片資訊
            this.Label1.Text = String.Format("Face : 找到{0}張臉, {1}男 {2}女", Results.Faces.Count(), isM, isF);
            this.Label2.Text = "說明 : " + Results.Description.Captions[0].Text;

            //如果有找到臉，顯示有紅框的圖
            if (Results.Faces.Count() > 0)
            {
                //建立新檔案
                filename = Guid.NewGuid() + System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);
                bmp.Save(MapPath("pic/" + filename));
                //顯示新檔案
                this.Image1.ImageUrl = "pic/" + filename;
            }
        }
    }
}