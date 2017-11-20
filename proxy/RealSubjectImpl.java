package rideshare.framework.proxy;

import java.util.List;

public class RealSubjectImpl<Person> implements RealSubject<Person> {
	@Override
	public List<Person> request(SearchInput input) {
		return (List<Person>)new PersonDao().getPeople(input);
	}

}
