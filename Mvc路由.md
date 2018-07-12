## Mvc路由

在**RouteConfig.cs**中 系统会定义一个默认的路由规则

```
public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
```

### 一、IgnoreRoute

routes.IgnoreRoute是过滤指定的url路径,对于它的讲解可以参见http://blog.csdn.net/lvjin110/article/details/24638913、

这里可以先测试一下这个**IgnoreRoute**,再HomeController下面新增一个Action

```
public ActionResult TestIgnoreRoute()
        {
            ViewBag.Message = "测试IgnoreRoute";
            return View("Index");
        }
```

然后打开浏览器输入地址/home/TestIgnoreRoute后是可以显示页面的.

但是在RouteConfig的RegisterRoutes方法中加入

```
routes.IgnoreRoute("{home}/{TestIgnoreRoute}");
```

输入地址/home/TestIgnoreRoute，页面显示404。



### 二、路由匹配规则及顺序

MapRoute 是定义路由规则,这个可以定义多个,就拿系统默认定义的来说,它默认是匹配任何controller/action/id 模式的url。可以做几个测试。

首先新增一个MyController以及对应的视图(Views/My/Index.cshtml).并且在原来的home控制器中新增两个action.控制器代码入下:

```
 public class MyController : Controller
    {
        // GET: My
        public ActionResult Index(string id)
        {
            ViewBag.Message = "这是来之MyController的首页,传入的参数ID="+id;
            return View();
        }
    }
    
    --------------------HomeController---------------------------
    
     public ActionResult A()
     {
     ViewBag.Message = "测试默认路由A";
     return View("Index");
     }

    public ActionResult B()
    {
    ViewBag.Message = "测试默认路由B";
    return View("Index");
	}
```

因为有默认定义的路由的关系,所以在浏览器地址栏中输入:

```
http://localhost:54485     --匹配home/index
http://localhost:54485/home--匹配home/index
http://localhost:54485/home/a --匹配home/a
http://localhost:54485/home/b --匹配home/b
http://localhost:54485/b  --不匹配
http://localhost:54485/my  --匹配my/index
http://localhost:54485/my/index/5  --匹配my/index并且传入的参数id=5
http://localhost:54485/my/index?id=10 --匹配my/index并且传入的参数id=10
```

当然也可以自定义一些路由规则,路由规则由上而下的匹配顺序,一旦找到了匹配的路由,将会直接返回该Action对应的View.

修改默认的路由,给id参数一个默认值为5

```
 routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id ="5" }
            );
```

输入浏览器 /my/index 后 实际上匹配的是/my/index/5.

然后再添加一个路由MyRoute,这个路由在默认路由前面

```
 routes.MapRoute(
                name: "MyRoute",
                url: "My/{action}/{id}",//强制控制器是My
                defaults: new { controller = "My", action = "Index", id = "10" }
            );
            
  routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id ="5" }
            );
```



默认情况下:如果输入/My 那么匹配到的应该是/My/Index/5，但是因为加了一个自定义的路由，所以先回匹配到/My/Index/10



### 三、可变长度的路由

有一种情况是路由的参数不一定,可能是10个可能也是1个，如果在路由规则中定义10个参数来接受,那不累死了。所以有一种可变长度的路由。

首先,先测试一下地址栏输入/my/index/15/asd/123/bbb 会显示404,果然这种不定参数的url，默认路由是无法识别的，所以新增一个自定义路由:

```
 routes.MapRoute("MyRoute2", 
                "{controller}/{action}/{id}/{*aaa}", 
                new { controller = "Home", action = "Index", id = UrlParameter.Optional
                });
```

然后再次输入/my/index/15/asd/123/bbb，这样就可以匹配到/my/index/15了，而且后面的参数asd/123/bbb，可以用Request.Url 来获取



### 四、路由约束

输入一下路由:

```
/my/index/a   -- id=a
/my/index/1	 --id=1
/my/index/2017-1-1 --id=2017-1-1
```

这样的话对于参数id 来说，不能保证数据安全,假设我想控制id必须为最长为2个长度的数字呢？修改默认路由:

```c#
 routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id ="5" },
                constraints:new {
                    id = @"\d{1,2}"//只能使用一位或两位数字
                }
            );
```

这个路由规则可匹配

```c#
/my/index/1   --id=1
/my/index/12   --id=12
/my/index/123   --显示404
/my/index/a   ---显示404
```





