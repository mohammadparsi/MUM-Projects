package rideshare.framework.proxy.observer;

import java.util.ArrayList;
import java.util.List;



public class ResourceSubjectImpl<E> implements ResourceSubject<E> {
private E removedEntity;
	
	private List<ResourceObserver<E>> observers;
	private final Object MUTEX = new Object();

	public ResourceSubjectImpl() {
		observers = new ArrayList<ResourceObserver<E>>();
	}

	@Override
	public void attach(ResourceObserver<E> observer) {
		synchronized (MUTEX) {
//			if (!observers.contains(observer))
				observers.add(observer);
		}
	}

	@Override
	public void detach(ResourceObserver<E> observer) {
		synchronized (MUTEX) {
			int i = observers.indexOf(observer);
			if (i >= 0)
				observers.remove(i);
		}
	}

	@Override
	public void notifyObservers() {
		synchronized (MUTEX) {
			int n = observers.size();
//			for (int i = 0; i < n; ++i) {
				ResourceObserver<E> observer = (ResourceObserver<E>) observers.get(0);
				observer.updateCache(removedEntity);
//			}
		}
	}
	
	public void setRemovedEntity(E removedEntity) {
		this.removedEntity = removedEntity;
		notifyObservers();
	}

}
