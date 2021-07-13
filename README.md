# Gallery

A proof-of-concept for a simple single-page web application that functions as a
photo gallery.

## Build

A dotnet-publish script is provided and may be run using:

```bash
$ ./publish

+ dotnet publish --configuration=Release -p:PublishProfile=Default
Microsoft (R) Build Engine version 17.0.0-preview-21302-02+018bed83d for .NET
Copyright (C) Microsoft Corporation. All rights reserved.

  Determining projects to restore...
  Restored /home/adyavanapalli/Repos/Gallery/Gallery.csproj (in 571 ms).
  You are using a preview version of .NET. See: https://aka.ms/dotnet-core-preview
  Gallery -> /home/adyavanapalli/Repos/Gallery/bin/Release/net6.0/linux-x64/Gallery.dll
  Gallery -> /home/adyavanapalli/Repos/Gallery/
```

## Prerequisites For Running

Before the running the web application, a running Postgres instance at
`localhost:5432` is expected to be running. There's a script available for
initializing this database using Docker: [`init.sh`](Database/Docker/init.sh).
In the script, a docker instance running Postgres is initialized and the schema
file ([`images.sql`](Database/Schema/images.sql)) is applied to the instance:

```bash
$ ./Database/Docker/init.sh

+ sudo docker run --detach --env POSTGRES_USER=adyavanapalli --env POSTGRES_PASSWORD=postgres --publish 127.0.0.1:5432:5432 postgres
2437626ff6bdac7a241c3dde99ad73bfb4452210d69da28d6ff4d8a0756ccc84
+ sleep 10s
++ find . -name images.sql
+ PGPASSWORD=postgres
+ psql --host=localhost --file=./Database/Schema/images.sql
CREATE TABLE
COMMENT
COMMENT
COMMENT
COMMENT
COMMENT
```

## Running

After building the project, it may be run using:

```bash
$ ./Gallery

dbug: Microsoft.Extensions.Hosting.Internal.Host[1]
      Hosting starting
dbug: Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderFactory[12]
      Registered model binder providers, in the following order: Microsoft.AspNetCore.Mvc.ModelBinding.Binders.BinderTypeModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.ServicesModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.BodyModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.HeaderModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.FloatingPointTypeModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.EnumTypeModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.DateTimeModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.SimpleTypeModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.CancellationTokenModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.ByteArrayModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.FormFileModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.FormCollectionModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.KeyValuePairModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.DictionaryModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.ArrayModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.CollectionModelBinderProvider, Microsoft.AspNetCore.Mvc.ModelBinding.Binders.ComplexObjectModelBinderProvider
dbug: Microsoft.AspNetCore.Server.Kestrel.Core.KestrelServer[0]
      Using development certificate: CN=localhost (Thumbprint: AFAAE284CFFB29BF45E9E9D4E77912D1C16C2D16)
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://localhost:8443
dbug: Microsoft.AspNetCore.Hosting.Diagnostics[13]
      Loaded hosting startup assembly Gallery
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Development
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /home/adyavanapalli/Repos/Gallery
dbug: Microsoft.Extensions.Hosting.Internal.Host[2]
      Hosting started
```
