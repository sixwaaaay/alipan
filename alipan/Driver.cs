namespace alipan;

public class Driver(HttpClient httpClient)
{
    /// <summary>
    /// 获取阿里云盘文件列表
    /// </summary>
    /// <returns></returns>
    public async Task<FileListResp> GetFileListAsync(FileListReq req, CancellationToken token = default) =>
        await httpClient.Request(HttpMethod.Post, "/adrive/v1.0/openFile/list", req,Context.Default.FileListReq, Context.Default.FileListResp, token)
            .ConfigureAwait(false);
    
    /// <summary>
    /// 文件搜索
    /// </summary>
    public async Task<FileListResp> SearchFileAsync(StarredFileReq req, CancellationToken token = default) =>
        await httpClient.Request(HttpMethod.Post, "/adrive/v1.0/openFile/search", req, Context.Default.StarredFileReq, Context.Default.FileListResp, token)
            .ConfigureAwait(false);
    
    /// <summary>
    /// 获取收藏列表
    /// </summary>
    public async Task<FileListResp> GetStarFileListAsync(FileListReq req, CancellationToken token = default) =>
        await httpClient.Request(HttpMethod.Post, "/adrive/v1.0/openFile/starredList", req, Context.Default.FileListReq, Context.Default.FileListResp, token)
            .ConfigureAwait(false);
    
    
    /// <summary>
    /// 获取文件信息
    /// </summary>
    public async Task<FileDetailResp> GetFileInfoAsync(FileDetailReq req, CancellationToken token = default) =>
        await httpClient.Request(HttpMethod.Post, "/adrive/v1.0/openFile/get", req, Context.Default.FileDetailReq, Context.Default.FileDetailResp, token)
            .ConfigureAwait(false);
    
    /// <summary>
    /// 获取文件下载地址
    /// </summary>
    public async Task<DownloadUrlResp> GetFileDownloadUrlAsync(DownloadUrlReq req, CancellationToken token = default) =>
        await httpClient.Request(HttpMethod.Post, "/adrive/v1.0/openFile/downloadUrl", req, Context.Default.DownloadUrlReq, Context.Default.DownloadUrlResp, token)
            .ConfigureAwait(false);
}