using System;
using System.Collections.Generic;


public class ProductAd
{
    public string Id { get; set; }
    public int Rank { get; set; }
    public string Position { get; set; }
    public string ImpressionToken { get; set; }
    public Offer Offer { get; set; }
    public List<EntityInfo> EntityInfos { get; set; }
    public PrimaryImageInfo PrimaryImageInfo { get; set; }
}

public class Offer
{
    public string Url { get; set; }
    public string UrlPingSuffix { get; set; }
    public Seller Seller { get; set; }
    public double Price { get; set; }
    public string PriceCurrency { get; set; }
    public ItemOffered ItemOffered { get; set; }
    public string PriceCurrencySymbol { get; set; }
}

public class Seller
{
    public string Name { get; set; }
    public string Domain { get; set; }
}

public class ItemOffered
{
    public string Name { get; set; }
    public ImageInfo Image { get; set; }
    public string Description { get; set; }
}

public class ImageInfo
{
    public string ContentUrl { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

public class EntityInfo
{
    public string EntityIdType { get; set; }
    public List<string> EntityIds { get; set; }
}

public class PrimaryImageInfo
{
    public ColorInfo ColorInfo { get; set; }
}

public class ColorInfo
{
    public string BackgroundColor { get; set; }
    public string LayoutColor { get; set; }
    public string FontColor { get; set; }
}

public class AdsResponse
{
    public string Type { get; set; }
    public Instrumentation Instrumentation { get; set; }
    public QueryContext QueryContext { get; set; }
    public List<ProductAd> Value { get; set; }
}

public class Instrumentation
{
    public string Type { get; set; }
    public string PingUrlBase { get; set; }
    public string PageLoadPingUrl { get; set; }
}

public class QueryContext
{
    public string OriginalQuery { get; set; }
}
