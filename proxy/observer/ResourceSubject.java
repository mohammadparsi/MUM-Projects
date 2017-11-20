package rideshare.framework.proxy.observer;

public interface ResourceSubject<E> {
	public void attach(ResourceObserver<E> observer);
	public void detach(ResourceObserver<E> observer);
	public void notifyObservers();
}
