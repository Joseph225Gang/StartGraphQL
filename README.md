# StartGraphQL
Asp.net Core GraphQL 練習
主要參考 
初探GraphQL - 昕力大學(https://tpu.thinkpower.com.tw/tpu/articleDetails/1326)
server cannot be reached 

services.Configure<IISServerOptions>(options =>
        {
            options.AllowSynchronousIO = true;
        });
解決
