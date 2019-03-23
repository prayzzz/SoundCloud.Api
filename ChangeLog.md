As of March 2019 it's not possible to register apps on soundcloud.com.
Therefore it's not possible to request OAuth tokens.
Every endpoint which requires Authentication is not usable at the moment.
This includes:
* whole `Me` Endpoint
* every Create, Update, Delete request

You can only access public content!

I'm not actively looking if this has changed. Please notify me it it did.

**Breaking Changes**
* removed `ISoundCloudClient.IsAuthorized`
* removed all synchronous methods
* removed Groups Endpoints

**Changes**
* moved to IHttpClientFactory for requests 

**Todo**
* UserAgent for DefaultHttpClientFactory
* appendCredentialProperties
* async SoundCloudList.Get
* return object cleanup? ApiResponse, WebResponse...