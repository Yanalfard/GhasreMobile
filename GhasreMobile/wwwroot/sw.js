const staticCacheName = 'site-static-v0.0.1';
const isOnline = navigator.onLine;

const assets = [
    '/favicon.ico'
];

// install the service worker
self.addEventListener('install', (evt) => {
    evt.waitUntil(
        caches.open(staticCacheName).then(cache => {
            for (let item of assets) {
                cache.add(item);
            }
        })
    )
});

// activate service worker
self.addEventListener('activate', (evt) => {
    evt.waitUntil(
        caches.keys().then(keys => {
            return Promise.all(keys
                .filter(key => key !== staticCacheName)
                .map(key => caches.delete(key)))
        })
    )
});

// intercept caches
self.addEventListener('fetch', (evt) => {

    if (!(evt.request.url.indexOf('http') === 0)) return;

    evt.respondWith(
        caches.match(evt.request)
            .then(cacheRes => {
                return cacheRes
                    || // not in caches
                    fetch(evt.request).then(fetchRes => {

                        // if the url is a resource, cache it
                        if (evt.request.url.includes('/css/')
                            || evt.request.url.includes('/fonts/')
                            || evt.request.url.includes('/js/')
                            || evt.request.url.includes('/st/')
                            || evt.request.url.includes('/dy/')) {
                            return caches.open(staticCacheName).then(cache => {
                                cache.put(evt.request.url, fetchRes.clone());

                                return fetchRes;
                            })
                        }

                        return fetchRes;
                    })
            })
    )
})