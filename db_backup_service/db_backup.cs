using System.ServiceProcess;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System;
using System.Timers;

namespace db_backup_service
{
    public partial class db_backup : ServiceBase
    {
        private string strConn = string.Empty;
        private string strDBName = string.Empty;
        private string strBackupPath = string.Empty;
        private Timer timer;
        private int nTimeInterval = 10000;
        public db_backup()
        {
            InitializeComponent();
            try
            {
                strConn = ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString;
                foreach (string key in ConfigurationManager.AppSettings)
                {
                    if (key.ToLower() == "dbname")
                    {
                        strDBName = ConfigurationManager.AppSettings[key];
                    }
                    if (key.ToLower() == "interval")
                    {
                        nTimeInterval = Convert.ToInt32(ConfigurationManager.AppSettings[key]);
                    }
                }
                strBackupPath = AppDomain.CurrentDomain.BaseDirectory + @"\DBBackUp\";
                if (!Directory.Exists(strBackupPath))
                {
                    Directory.CreateDirectory(strBackupPath);
                }
            }
            catch
            {
                strConn = string.Empty;
            }
        }


        private bool BackUp_DB()
        {
            bool boResult = false;
            try
            {
                if (string.IsNullOrEmpty(strConn))
                { return boResult; }

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = strConn;
                    conn.Open();

                    string strBackUpFileName = DateTime.Now.ToString("yyyyMMdd") + ".bak";
                    if (!File.Exists(strBackupPath + strBackUpFileName))
                    {
                        string strSQLBackUp = "BackUp Database " + strDBName + " TO DISK = '" + strBackupPath + strBackUpFileName + "' WITH FORMAT, MEDIANAME = 'SQLSERVERBACKUPS', NAME = 'FULL Backup of " + strDBName + "'";

                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = strSQLBackUp;
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                        boResult = true;
                    }
                }
            }
            catch (Exception ex)
            {
                FileStream fs = new FileStream(@"C:\Xiaolj\Bounds\db_backup_service\bin\Debug\Log.txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(string.Format("数据备份出现错误： {0} \n", ex.Message));
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            return boResult;
        }
        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = nTimeInterval;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            BackUp_DB();
        }

        protected override void OnStop()
        {
            timer.Stop();
            timer.Dispose();
        }
    }
}
