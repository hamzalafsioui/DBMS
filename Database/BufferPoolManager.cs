using System.Text.Json;

namespace Database;

/// <summary>
/// Buffer Pool Manager - Manages in-memory data with dirty page tracking
/// </summary>
public class BufferPoolManager
{
    private readonly DiskManager _diskManager;
    private readonly string _database;
    private DatabasePage _page;
    private bool _isDirty = false;

    public BufferPoolManager(string database)
    {
        _database = database;
        _diskManager = new DiskManager();

        if (_diskManager.Exists(database))
        {
            _page = _diskManager.Read(database);
        }
        else
        {
            _page = new DatabasePage();
            _diskManager.Write(database, _page);
        }
    }

    /// <summary>
    /// Flush dirty pages to disk if needed
    /// </summary>
    private DatabasePage ReadOrWriteOnDisk()
    {
        if (_isDirty)
        {
            _diskManager.Write(_database, _page);
            _isDirty = false;
        }
        return _page;
    }

   
}
