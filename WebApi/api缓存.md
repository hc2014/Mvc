nuget 搜索**Strathweb.CacheOutput.WebApi2** 

在接口上添加特性

```c#
[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
public IEnumerable<string> Get()
{
  return new string[] { "value1", "value2" };
}
```

缓存是60秒，重复请求的情况下 是不会进入Get()方法的，而是直接返回了 上一次请求的返回值