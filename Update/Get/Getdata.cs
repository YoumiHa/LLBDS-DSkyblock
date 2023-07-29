using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LanZouCloudAPI;
using Skyblock.Logger;
using System.Threading.Tasks;


namespace Skyblock.Update.Get
{
    internal class Getdata
       
    {

        public LanZouCloud lanzou = new LanZouCloud();
        public async Task DownloadFileBigByUrl1(string x, string share,string code,string dir)
        {
            
           await  lanzou.DownloadFileByUrl(share,"./logs","string.exe", code, true, new Progress<ProgressInfo>(_progress =>
           {
               if (_progress.state == ProgressState.Start)
                   logger.Info("文件下载准备中!");
            
               if (_progress.state == ProgressState.Ready)
                   logger.Info("文件下载开始!");
               if (_progress.state == ProgressState.Progressing)
                   Console.Write("\r已下载:{0} ", _progress.current + "/" + _progress.total);
               if (_progress.state == ProgressState.Finish)
               {
            
                   Console.WriteLine("\n ");
                   logger.Warn("下载完成");
                   try
                   {
                       File.Move($"./logs/{_progress.fileName}", $"{dir}/{x}");
                   }
                   catch (Exception ex)
                   {
                       logger.Error(ex);
                   }
               }
                     
           }));
            
        }


    }
}
