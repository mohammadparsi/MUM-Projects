package rideshare.framework.proxy.observer;

public interface ResourceObserver<E> {
	public void updateCache(E removedEntity);
}
