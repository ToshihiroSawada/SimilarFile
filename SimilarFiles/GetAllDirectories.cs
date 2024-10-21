using NLog;
using System.Text.RegularExpressions;

namespace SimilarFiles
{
    internal class GetAllDirectories
    {
        bool _PSKILL = Globals.PSKILL;
        readonly Logger _logger = Globals.LOGGER;

        private async Task<List<string>> GetAllDir(List<string> argList)
        {
            //ファイルハッシュを取得しないスキップリスト
            var skipList = new List<string> { @"C:\Users\Public", @"C:\Users\Default", @"C:\Users\All Users", @"C:\Windows", @"C:\Power_On_and_WOL", @"C:\Intel", @"C:\Driver", @"C:\Program Files", @"C:\Program Files (x86)", @"C:\ProgramData", @"C:\Recovery", "System Volume Information" };
            //var skipList = new List<string>();
            var pattern = @".*AppData.*|.*\$.*";
            var list = new List<string>();
            var regex = new Regex(pattern);
            var directories = new List<string>();

            foreach (var path in argList)
            {
                var matchFlug = false;
                try
                {
                    if (regex.IsMatch(path))
                    {
                        continue;
                    }

                    foreach (var i in skipList)
                    {
                        if (path.Contains(i))
                        {
                            matchFlug = true;
                        }
                    }
                }
                catch (Exception err)
                {
                    _logger.Debug(err.ToString());
                    continue;
                }
                if (matchFlug)
                {
                    continue;
                }

                list.Add(path);

                try
                {
                    directories.AddRange(Directory.GetDirectories(
                        path, "*", SearchOption.TopDirectoryOnly
                    ));
                }
                catch (Exception err)
                {
                    _logger.Error(err, path);
                    continue;
                }
                if (_PSKILL == true)
                {
                    return list;
                }
            }
            if (directories.Count > 0)
            {
                var data = await Task.Run(() => GetAllDir(directories));
                list.AddRange(data);
            }
            return list;
        }


    }
}
