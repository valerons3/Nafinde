using System;
using System.IO;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Nafinde;

public static class Provider
{
    public static async Task WriteID(long ID)
    {
        List<long> usersID = await ReadUsersID();
        usersID.Add(ID);
        try
        {
            await WriteUsersID(usersID);
        }
        catch { return; }
    }
    public static async Task RemoveID(long ID)
    {
        List<long> usersID = await ReadUsersID();
        usersID.Remove(ID);
        try
        {
            await WriteUsersID(usersID);
        }
        catch { return; }
    }
    public static async Task<bool> CheckID(long ID)
    {
        List<long> usersID = await ReadUsersID();
        return usersID.Contains(ID);
    }
    private static async Task<List<long>> ReadUsersID()
    {
        List<long> usersID;
        using (FileStream fs = new FileStream(PathManager.PathUserID, FileMode.Open))
        {
            usersID = await JsonSerializer.DeserializeAsync<List<long>>(fs);
        }
        return usersID;
    }
    private static async Task WriteUsersID(List<long> usersID)
    {
        using (FileStream fs = new FileStream(PathManager.PathUserID, FileMode.Create))
        {
            await JsonSerializer.SerializeAsync<List<long>>(fs, usersID);
        }
    }
}
