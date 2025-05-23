using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GEO_DROID.Resources.Lib.Extra
{
    public class LogFile : IDisposable
    {
        private string folder = string.Empty;
        private string filename = string.Empty;

        protected FileStream stream = null;
        protected StreamWriter file = null;
        protected StringBuilder sb = new StringBuilder();

        public bool Enabled = true;

        public enum LogByDate
        {
            Disable, Day, Week, Month
        }

        public LogFile()
        {
            this.Enabled = false;
        }

        public LogFile(string folder, string filename, LogByDate logByDate = LogByDate.Disable, int linesCached = 0)
        {
            _linesCount = 0;
            setFile(folder, filename, logByDate, linesCached);
        }

        public void setFile(string folder, string filename, LogByDate logByDate = LogByDate.Disable, int linesCached = 0)
        {
            this.folder = Path.Combine(Config.pathLog, folder);
            this.filename = filename;
            this.linesCached = linesCached;
            this.logByDate = logByDate;

            open();
        }

        public void Dispose()
        {
            close();
        }

        #region Open/Close

        public LogByDate logByDate = LogByDate.Disable;
        private DateTime currentDateFolder;
        private string localFilename;

        public bool useDayFolder
        {
            get { return logByDate != LogByDate.Disable; }
        }


        private string buildFilename()
        {
            if (useDayFolder)
            {
                // Creamos una Ruta "/#LOG#/Folder/#DATETIME#/filename.log"
                //
                // Y nos guardamos la fecha de ese #DATETIME# y cuando detectamos que cambia el dia cerramos y 
                // abrimos el log de nuevo para crear otro archivo (en lugar de hacer cambios cada dia, tenemos un contador de 'dayFolderCount')
                // asi podemos hacer logs diarios/semanales/15 dias .. como queramos

                currentDateFolder = DateTime.Now;
                string currentDateFolderStr = string.Format("{0:yyyy.MM.dd}", currentDateFolder);


                localFilename = Path.Combine(folder, currentDateFolderStr);
                if (!Directory.Exists(localFilename))
                    Directory.CreateDirectory(localFilename);

                localFilename = Path.Combine(localFilename, filename);
                return localFilename;
            }
            else
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                localFilename = Path.Combine(folder, filename);

                return localFilename;
            }
        }

        private void open()
        {
            if (string.IsNullOrEmpty(localFilename))
                buildFilename();

            // Construimos la ruta del archivo
            stream = File.Open(localFilename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            file = new StreamWriter(stream);
        }


        private bool isNewDate()
        {
            if (useDayFolder)
            {
                switch (logByDate)
                {
                    case LogByDate.Day:
                        return (DateTime.Now.Day != currentDateFolder.Day);

                    case LogByDate.Week:
                        int f1 = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                        int f2 = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(currentDateFolder, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
                        return (f1 != f2);

                    case LogByDate.Month:
                        return (DateTime.Now.Month != currentDateFolder.Month);
                }

                return false;
            }
            else
                return false;
        }

        private void reopen()
        {
            if (!isNewDate()) return;
            if (string.IsNullOrEmpty(filename))
                return;

            string currentFile = localFilename;     // Archivo actual
            string newFile = buildFilename();   // nuevo nombre

            // 1- Cerramos el archivo actual (e indicamos cual es el proximo archivo)
            WriteRaw("## ----------------------");
            WriteRaw("## New Log: Siguiente archivo: " + newFile);
            WriteRaw("## ----------------------");
            close();

            // 2 - Abrimos un nuevo archivo
            open();
            WriteRaw("## ----------------------");
            WriteRaw("## New Log: Antrerior archivo: " + currentFile);
            WriteRaw("## ----------------------");

            flush();
        }

        private void close()
        {
            if (!Enabled) return;

            try
            {
                if (file == null) return;

                lock (this)
                {
                    flush();
                    file.Dispose();
                    file = null;

                    stream.Dispose();
                    stream = null;
                }
            }
            catch (Exception) { }

        }
        #endregion

        #region Control de lineas / Flush
        public int linesCached = 0;
        private int _linesCount = 0;
        public int linesCount
        {
            get { return _linesCount; }
            set
            {
                _linesCount = value;

                // segun las lineas cacheadas
                if (linesCount > linesCached)
                    flush();
            }
        }

        public void flush()
        {
            lock (this)
            {
                if (file != null)
                {
                    file.Write(sb.ToString());
                    file.Flush();
                }

                sb.Clear();
                _linesCount = 0;    // Importante, resetear la variable sin llamar al metodo
            }

        }
        #endregion

        public void Write(string message)
        {
            if (!Enabled) return;

            // si hay un cambio de dia entonces reabrimos el archivo
            // esto salva el log actual y abre uno nuevo con la nueva fecha
            if (isNewDate())
            {
                reopen();
            }

            // Escribimos
            WriteRaw(message);
            linesCount++;
        }

        private void WriteRaw(string message)
        {
            if (!Enabled) return;

            lock (sb)
            {
                sb.AppendLine(string.Format("[{0}] {1}",
                    string.Format("{0:dd/MM/yyyy HH:mm:ss.fff}", DateTime.Now),
                    message));
            }
        }

    }
}
