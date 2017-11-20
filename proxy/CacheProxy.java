package rideshare.framework.proxy;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Random;

public class CacheProxy<E> implements RealSubject<E> {
	private Integer CacheCapacity = 10000;
	private RealSubject<E> realSubject;
	private HashMap<Object, List<E>> cache = new HashMap<>();
	List<E> cachedEntities = new ArrayList<E>();

	public CacheProxy() {
		realSubject = new RealSubjectImpl<E>();
	}

	public List<E> request(SearchInput input) {
		cachedEntities = (List<E>) cache.get(input.concatinateSearchKeywords());
		if (cachedEntities == null) {
			List<E> entities = realSubject.request(input);
			if (cache.size() > CacheCapacity) {
				removeEntryFromCache();
			}
			addEntryToCache(input.concatinateSearchKeywords(), entities);
			return entities;
		} else {
			System.out.println("entry not found in cache");
		}
		return cachedEntities;
	}

	public Integer getCacheCapacity() {
		return CacheCapacity;
	}

	public void setCacheCapacity(Integer cacheCapacity) {
		CacheCapacity = cacheCapacity;
	}

	private void addEntryToCache(String key, List<E> cacheEntry) {
		cache.put(key.toString(), cacheEntry);					
	}

	private void removeEntryFromCache() {
		Random random = new Random();
		List<Object> keys = new ArrayList<Object>(cache.keySet());
		Object randomKey = keys.get(random.nextInt(keys.size()));
		cache.remove(randomKey);
	}
}
