using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace StockControl
{
    public class StockControlItem
    {
        public string Name { get; set; }

        public string PriceList { get; set; }
        public bool PriceListExists { get { return File.Exists(PriceList); } }

        public string Feed { get; set; }
        private string FeedFile { get; set; }
        public bool IsUrl { get; set; }
        public bool GetFeedFile
        {
            get
            {
                if (IsUrl)
                    DownloadFeed();

                return File.Exists(FeedFile);
            }
        }


        public string CatalogNumber { get; set; }

        public StockControlItem(string name, string priceList, string catalogNumber, string feed, bool isUrl)
        {
            if (!(!string.IsNullOrEmpty(name) && name.IndexOfAny(Path.GetInvalidFileNameChars()) < 0))
                return;

            this.Name = name;
            this.PriceList = priceList;
            this.Feed = feed;
            this.FeedFile = feed;
            this.IsUrl = isUrl;
            this.CatalogNumber = catalogNumber;


            if (stockControlList.ContainsKey(this.Name))
            {
                if (MessageBox.Show("Položka pod tímto názvem již existuje. Chcete ji přepsat?", "Položka již existuje", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
                stockControlList[this.Name] = this;
            }
            else
                stockControlList.Add(this.Name, this);
        }



        public static Dictionary<string, StockControlItem> stockControlList = new Dictionary<string, StockControlItem>();

        public void Run()
        {
            #region Excel Open
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook xlWorkBook = xlApp.Workbooks.Open(PriceList);
            Excel.Worksheet xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            object misValue = System.Reflection.Missing.Value;
            #endregion



            #region Excel Close
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);
            #endregion
        }

        public void DownloadFeed()
        {
            string fileName = Name;



            this.FeedFile = fileName;
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
