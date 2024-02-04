using System.Text.Json;
using System.Text.Json.Serialization;

namespace alipan;

public record User
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public record Drive
{
    [JsonPropertyName("user_id")] public string UserId { get; set; } = string.Empty;
    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("avatar")] public string Avatar { get; set; } = string.Empty;

    [JsonPropertyName("default_drive_id")] public string DefaultDriveId { get; set; } = string.Empty;

    [JsonPropertyName("resource_drive_id")]
    public string? ResourceDriveId { get; set; }

    [JsonPropertyName("backup_drive_id")] public string? BackupDriveId { get; set; }
}

public record Space
{
    [JsonPropertyName("used_size")] public long UsedSize { get; set; }

    [JsonPropertyName("total_size")] public long TotalSize { get; set; }
}

public record SpaceInfo
{
    [JsonPropertyName("personal_space_info")]
    public Space Space { get; set; } = new();
}

public record AuthReq
{
    [JsonPropertyName("client_id")] public string ClientId { get; set; } = string.Empty;

    [JsonPropertyName("client_secret")] public string ClientSecret { get; set; } = string.Empty;

    [JsonPropertyName("grant_type")] public string GrantType { get; set; } = "refresh_token";

    [JsonPropertyName("refresh_token")] public string RefreshToken { get; set; } = string.Empty;
}

/// <summary>
///  获取文件列表请求
/// </summary>
public record FileListReq
{
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;

    [JsonPropertyName("limit")] public int Limit { get; set; } = 50;

    [JsonPropertyName("maker")] public string? Marker { get; set; }

    [JsonPropertyName("order_by")] public string OrderBy { get; set; } = "name";

    [JsonPropertyName("order_direction")] public string OrderDirection { get; set; } = "ASC";

    [JsonPropertyName("parent_file_id")] public string ParentFileId { get; set; } = string.Empty;

    [JsonPropertyName("category")] public string? Category { get; set; }

    [JsonPropertyName("type")] public string? Type { get; set; }

    [JsonPropertyName("image_thumbnail_time")]
    public string? ImageThumbnailTime { get; set; }

    [JsonPropertyName("video_thumbnail_width")]
    public string? VideoThumbnailWidth { get; set; }

    [JsonPropertyName("video_thumbnail_height")]
    public string? VideoThumbnailHeight { get; set; }

    [JsonPropertyName("fields")] public string? Fields { get; set; }
}

/// <summary>
///  文件列表响应
/// </summary>
public record FileListResp
{
    [JsonPropertyName("items")] public List<FileItem> Items { get; set; } = new();

    [JsonPropertyName("next_marker")] public string? NextMarker { get; set; } = default;


    public record FileItem
    {
        /// <summary>
        ///  盘id
        /// </summary>
        [JsonPropertyName("drive_id")]
        public string DriveId { get; set; } = string.Empty;

        /// <summary>
        ///  文件id
        /// </summary>
        [JsonPropertyName("file_id")]
        public string FileId { get; set; } = string.Empty;

        /// <summary>
        ///  父文件夹id
        /// </summary>
        [JsonPropertyName("parent_file_id")]
        public string ParentFileId { get; set; } = string.Empty;

        /// <summary>
        ///  文件名
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 文件大小
        /// </summary>
        [JsonPropertyName("size")]
        public int Size { get; set; }

        /// <summary>
        ///  文件类型
        /// </summary>
        [JsonPropertyName("file_extension")]
        public string FileExtension { get; set; } = string.Empty;

        /// <summary>
        ///  文件哈希
        /// </summary>
        [JsonPropertyName("content_hash")]
        public string ContentHash { get; set; } = string.Empty;

        /// <summary>
        ///  文件分类
        /// </summary>
        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        ///  类型
        /// </summary>
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        ///  缩略图
        /// </summary>
        [JsonPropertyName("thumbnail")]
        public string? Thumbnail { get; set; }

        /// <summary>
        ///  图片预览地址
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        /// created_at
        /// </summary>
        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; } = string.Empty;

        /// <summary>
        /// updated_at
        /// </summary>
        [JsonPropertyName("updated_at")]
        public string UpdatedAt { get; set; } = string.Empty;

        /// <summary>
        ///  播放游标
        /// </summary>
        [JsonPropertyName("play_cursor")]
        public string? PlayCursor { get; set; }

        /// <summary>
        /// 视频媒体元数据
        /// </summary>
        [JsonPropertyName("video_media_metadata")]
        public string? VideoMediaMetadata { get; set; }

        /// <summary>
        /// 视频预览元数据
        /// </summary>
        [JsonPropertyName("video_preview_metadata")]
        public string? VideoPreviewMetadata { get; set; }
    }
}

/// <summary>
///  文件搜索请求
/// </summary>
public class FileSearchReq
{
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;

    [JsonPropertyName("limit")] public int Limit { get; set; } = 50;

    ///<summary>
    /// 查询语句，样例：
    ///  固定目录搜索，只搜索一级 parent_file_id = '123'
    /// 精确查询 name = '123'
    ///  模糊匹配 name match "123"
    ///  搜索指定后缀文件 file_extension = 'apk' 
    ///  范围查询 created_at &lt; "2019-01-14T00:00:00"
    ///  复合查询:
    ///  type = 'folder' or name = '123'
    ///  parent_file_id = 'root' and name = '123' and category = 'video'
    ///</summary>
    [JsonPropertyName("query")]
    public string Query { get; set; } = string.Empty;

    [JsonPropertyName("marker")] public string? Marker { get; set; }

    [JsonPropertyName("order_by")] public string OrderBy { get; set; } = "created_at ASC";

    [JsonPropertyName("parent_file_id")] public string ParentFileId { get; set; } = string.Empty;

    [JsonPropertyName("image_thumbnail_time")]
    public string? ImageThumbnailTime { get; set; }

    [JsonPropertyName("video_thumbnail_width")]
    public string? VideoThumbnailWidth { get; set; }

    [JsonPropertyName("video_thumbnail_height")]
    public string? VideoThumbnailHeight { get; set; }

    [JsonPropertyName("fields")] public string? Fields { get; set; }
}

public record StarredFileReq
{
    // drive_id
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;

    // limit
    [JsonPropertyName("limit")] public int Limit { get; set; } = 50;
    // marker

    [JsonPropertyName("marker")] public string? Marker { get; set; }

    // order_by
    [JsonPropertyName("order_by")] public string OrderBy { get; set; } = "name";

    // video_thumbnail_time, video_thumbnail_width, image_thumbnail_time
    [JsonPropertyName("image_thumbnail_time")]
    public string? ImageThumbnailTime { get; set; }

    [JsonPropertyName("video_thumbnail_width")]
    public string? VideoThumbnailWidth { get; set; }

    [JsonPropertyName("video_thumbnail_height")]
    public string? VideoThumbnailHeight { get; set; }

    // order_direction
    [JsonPropertyName("order_direction")] public string OrderDirection { get; set; } = "ASC";

    // type
    [JsonPropertyName("type")] public string? Type { get; set; }
}

public record FileDetailReq
{
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;

    [JsonPropertyName("file_id")] public string FileId { get; set; } = string.Empty;
    
    // video_thumbnail_time, video_thumbnail_width, image_thumbnail_time
    [JsonPropertyName("image_thumbnail_time")]
    public string? ImageThumbnailTime { get; set; }

    [JsonPropertyName("video_thumbnail_width")]
    public string? VideoThumbnailWidth { get; set; }

    [JsonPropertyName("video_thumbnail_height")]
    public string? VideoThumbnailHeight { get; set; }

    [JsonPropertyName("fields")] public string? Fields { get; set; }
}


public record FileDetailResp
{
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;

    [JsonPropertyName("file_id")] public string FileId { get; set; } = string.Empty;

    [JsonPropertyName("parent_file_id")] public string ParentFileId { get; set; } = string.Empty;

    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("size")] public int Size { get; set; }

    [JsonPropertyName("file_extension")] public string FileExtension { get; set; } = string.Empty;

    [JsonPropertyName("content_hash")] public string ContentHash { get; set; } = string.Empty;

    [JsonPropertyName("category")] public string Category { get; set; } = string.Empty;

    // type,thumbnail, url, created_at, updated_at
    [JsonPropertyName("type")] public string Type { get; set; } = string.Empty;

    [JsonPropertyName("thumbnail")] public string? Thumbnail { get; set; }

    [JsonPropertyName("url")] public string? Url { get; set; }

    [JsonPropertyName("created_at")] public string CreatedAt { get; set; } = string.Empty;

    [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;

}


public record PathSearch
{
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;

    [JsonPropertyName("file_path")] public string FilePath { get; set; } = string.Empty;
}

public record DownloadUrlReq
{
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;

    [JsonPropertyName("file_id")] public string FileId { get; set; } = string.Empty;
    
    [JsonPropertyName("expire_sec")] public int ExpireSec { get; set; } = 900;
}

public record DownloadUrlResp
{

    [JsonPropertyName("url")] public string Url { get; set; } = string.Empty;

    [JsonPropertyName("expiration")] public string Expiration { get; set; } = string.Empty;
    
    [JsonPropertyName("method")] public string Method { get; set; } = "GET";
}





public record FileUploadReq
{
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;

    [JsonPropertyName("parent_file_id")] public string ParentFileId { get; set; } = string.Empty;

    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")] public string Type { get; set; } = "file";

    [JsonPropertyName("check_name_mode")] public string CheckNameMode { get; set; } = "auto_rename";

    [JsonPropertyName("local_modified_at")]
    public string? LocalModifiedAt { get; set; } = string.Empty; // "2021-08-31T15:00:00.000Z"

    [JsonPropertyName("local_created_at")] public string? LocalCreatedAt { get; set; } = string.Empty;


    [JsonPropertyName("content_hash")]
    public string? ContentHash { get; set; } = string.Empty; // sha1 hash of file content

    [JsonPropertyName("content_hash_name")]
    public string? ContentHashName { get; set; } = "sha1"; // sha1 hash algorithm

    [JsonPropertyName("proof_code")]
    public string? ProofCode { get; set; } = string.Empty; // quickly upload file need this

    [JsonPropertyName("proof_version")]
    public string? ProofVersion { get; set; } = "v1"; // quickly upload file need this
}

public record FileUploadResp
{
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;
    [JsonPropertyName("file_id")] public string FileId { get; set; } = string.Empty;
    [JsonPropertyName("status")] public string Status { get; set; } = string.Empty;
    [JsonPropertyName("parent_file_id")] public string ParentFileId { get; set; } = string.Empty;
    [JsonPropertyName("upload_id")] public string UploadId { get; set; } = string.Empty; // 创建文件夹返回空
    [JsonPropertyName("file_name")] public string FileName { get; set; } = string.Empty;
    [JsonPropertyName("available")] public bool? Available { get; set; }
    [JsonPropertyName("exist")] public bool? Exist { get; set; } // 是否存在同名文件
    [JsonPropertyName("rapid_upload")] public bool RapidUpload { get; set; } // 是否秒传
    [JsonPropertyName("part_info_list")] public List<UploadPartInfo> PartInfoList { get; set; } = [];

    public record UploadPartInfo
    {
        [JsonPropertyName("part_number")] public int PartNumber { get; set; }
        [JsonPropertyName("upload_url")] public string UploadUrl { get; set; } = string.Empty;
        [JsonPropertyName("part_size")] public int? PartSize { get; set; } = 1024;
    }
}

public record FileUploadUrlReq
{
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;

    [JsonPropertyName("parent_file_id")] public string ParentFileId { get; set; } = string.Empty;

    [JsonPropertyName("upload_id")] public string UploadId { get; set; } = string.Empty;
    [JsonPropertyName("part_info_list")] public List<UrlReqPartInfo> PartInfoList { get; set; } = new();

    public class UrlReqPartInfo
    {
        [JsonPropertyName("part_number")] public int PartNumber { get; set; }
    }
}

public record FileUploadUrlResp
{
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;
    [JsonPropertyName("file_id")] public string FileId { get; set; } = string.Empty;
    [JsonPropertyName("upload_id")] public string UploadId { get; set; } = string.Empty;
    [JsonPropertyName("created_at")] public string CreatedAt { get; set; } = string.Empty; // "2021-08-31T15:00:00.000Z
    [JsonPropertyName("part_info_list")] public List<UrlRespPartInfo> PartInfoList { get; set; } = [];

    public record UrlRespPartInfo
    {
        [JsonPropertyName("part_number")] public int PartNumber { get; set; }
        [JsonPropertyName("upload_url")] public string UploadUrl { get; set; } = string.Empty;
        [JsonPropertyName("part_size")] public int PartSize { get; set; } = 1024;
    }
}

public record FileListUploadedPartsReq
{
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;

    [JsonPropertyName("parent_file_id")] public string ParentFileId { get; set; } = string.Empty;

    [JsonPropertyName("upload_id")] public string UploadId { get; set; } = string.Empty;

    [JsonPropertyName("part_number_marker")]
    public string PartNumberMarker { get; set; } = string.Empty;
}

public record FileListUploadedPartsResp
{
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;
    [JsonPropertyName("upload_id")] public string UploadId { get; set; } = string.Empty;

    [JsonPropertyName("parallelUpload")] public bool ParallelUpload { get; set; }
    [JsonPropertyName("uploaded_parts")] public List<UploadedPartsPartInfo> UploadedParts { get; set; } = [];

    public record UploadedPartsPartInfo
    {
        [JsonPropertyName("etag")] public string Etag { get; set; } = string.Empty;
        [JsonPropertyName("part_number")] public int PartNumber { get; set; }
        [JsonPropertyName("part_size")] public int PartSize { get; set; } = 1024;
    }

    [JsonPropertyName("next_part_number_marker")]
    public string NextPartNumberMarker { get; set; } = string.Empty;
}

public record FileCompleteReq
{
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;

    [JsonPropertyName("file_id")] public string FileId { get; set; } = string.Empty;

    [JsonPropertyName("upload_id")] public string UploadId { get; set; } = string.Empty;
}

public record FileCompleteResp
{
    [JsonPropertyName("drive_id")] public string DriveId { get; set; } = string.Empty;

    [JsonPropertyName("file_id")] public string FileId { get; set; } = string.Empty;

    [JsonPropertyName("upload_id")] public string UploadId { get; set; } = string.Empty;

    [JsonPropertyName("name")] public string Name { get; set; } = string.Empty;

    [JsonPropertyName("size")] public int Size { get; set; }

    [JsonPropertyName("file_extension")] public string FileExtension { get; set; } = string.Empty;

    [JsonPropertyName("content_hash")] public string ContentHash { get; set; } = string.Empty;

    [JsonPropertyName("category")] public string Category { get; set; } = string.Empty;

    [JsonPropertyName("type")] public string Type { get; set; } = "file";

    [JsonPropertyName("thumbnail")] public string? Thumbnail { get; set; }

    [JsonPropertyName("url")] public string? Url { get; set; }

    [JsonPropertyName("download_url")] public string? DownloadUrl { get; set; }

    [JsonPropertyName("created_at")] public string CreatedAt { get; set; } = string.Empty;

    [JsonPropertyName("updated_at")] public string UpdatedAt { get; set; } = string.Empty;
}

[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.SnakeCaseLower)]
[JsonSerializable(typeof(Drive))]
[JsonSerializable(typeof(Space))]
[JsonSerializable(typeof(User))]
[JsonSerializable(typeof(SpaceInfo))]
[JsonSerializable(typeof(FileListReq))]
[JsonSerializable(typeof(FileSearchReq))]
[JsonSerializable(typeof(StarredFileReq))]
[JsonSerializable(typeof(FileDetailReq))]
[JsonSerializable(typeof(FileDetailResp))]
[JsonSerializable(typeof(PathSearch))]
[JsonSerializable(typeof(DownloadUrlReq))]
[JsonSerializable(typeof(DownloadUrlResp))]
[JsonSerializable(typeof(FileUploadReq))]
[JsonSerializable(typeof(FileUploadResp))]
[JsonSerializable(typeof(FileUploadUrlReq))]
[JsonSerializable(typeof(FileUploadUrlResp))]
[JsonSerializable(typeof(FileListUploadedPartsReq))]
[JsonSerializable(typeof(FileListUploadedPartsResp))]
[JsonSerializable(typeof(FileCompleteReq))]
[JsonSerializable(typeof(FileCompleteResp))]
[JsonSerializable(typeof(FileListResp))]
internal partial class Context : JsonSerializerContext;