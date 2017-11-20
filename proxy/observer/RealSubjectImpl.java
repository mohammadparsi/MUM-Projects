package rideshare.framework.proxy.observer;

import java.util.List;
import rideshare.framework.proxy.RealSubject;
import rideshare.framework.proxy.SearchInput;

public class RealSubjectImpl<Person> implements RealSubject<Person> {
	@Override
	public List<Person> request(SearchInput input) {
		// TODO Auto-generated method stub
		PersonDaoResource dao =PersonDaoResource.getSingletonInstance();
		return (List<Person>)dao.getPeople(input);
	}

}
