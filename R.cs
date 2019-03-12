using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace SUI
{
    /**
     * The resource class contains the  vital information about the program or module.
     * The values needs to working how static values.
     * v0008
     **/
    #region Not change
    public partial class R
    {
        public static string LOG => "RESX";
        public class Project
        {
            public static System.Reflection.Assembly assembly => System.Reflection.Assembly.GetExecutingAssembly();
            public static Version Version => assembly.GetName().Version;
            public static string Name => "SUI Library";
            public static string NS => "SUIL"; // Namespace
            public static int ID => Version.Major;
            public const string SP_PARAM = "KK_PARAM";
            public static string[] Resources => assembly.GetManifestResourceNames();
            public static ResourceManager LocationResx => new ResourceManager($"{typeof(R).Namespace}.content.Location{klib.R.Project.Language}", assembly);
        }
        /// <summary>
        /// SUI Library
        /// </summary>
        public static partial class Menus
        {
            public static string CompanyMenu => PreFix;
        }
        public static string PreFix => $"{klib.R.Project.Namespace}_{Project.NS}";

        /// <summary>
        /// Get file embedded. The file needs to save in context folder and build action embedded
        /// </summary>
        /// <param name="name">File name</param>
        /// <param name="cache">Get file loaded</param>
        /// <returns>Path the file</returns>
        public static FileInfo GetFile(string name, bool cache = true)
        {
            var appdata = klib.Shell.AppData(R.Project.Name, "resource");
            var path = System.IO.Path.Combine(appdata.FullName, name);

            if (System.IO.File.Exists(path) && cache)
                return new FileInfo(path);

            var res = Project.Resources.Where(t => t.Contains($".{name}")).First();

            using (Stream stream = Project.assembly.GetManifestResourceStream(res))
                klib.Shell.CreateFile(stream, new FileInfo(path), true);

            return new FileInfo(path);
        }
    }
    #endregion
    public partial class R
    {
        public static string CommandLineArg => "0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056";
        public static partial class Menus
        {
            //public static string Tools => $"{R.PreFix}_Tools";
        }
    }

    //public static class R
    //{
    //    
        
    //    public class Project
    //    {
    //        public static System.Reflection.Assembly assembly => System.Reflection.Assembly.GetExecutingAssembly();
    //        public static Version Version => assembly.GetName().Version;
    //        public static string Namespace = klib.R.Company.NS;
    //        public static int ID => Version.Major;
    //        public const string SP_PARAM = "KK_PARAM";
    //        public static string[] Resources => assembly.GetManifestResourceNames();
    //    }

    //    public static class Menus
    //    {
    //        public static string MenuFather => klib.R.Company.AliasName;
    //    }
    //}
}
