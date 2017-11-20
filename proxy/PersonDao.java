package rideshare.framework.proxy;

import java.util.ArrayList;
import java.util.List;

public class PersonDao {

	List<Person> people = new ArrayList<Person>();

	public PersonDao() {
		people.add(new Person("Mohammad", "Iran"));
		people.add(new Person("Tesfay", "Ethiopia"));
		people.add(new Person("Lwan", "Eritrea"));
		people.add(new Person("ChanPiseth", "Vietnam"));

		// TODO Auto-generated constructor stub
	}

	public List<Person> getPeople(SearchInput input) {
		List<Person> results = new ArrayList<Person>();
		for (Person person : people) {
			if (person.getName().equals(input.getSearchKeyword1()) && person.getCountry().equals(input.getSearchKeyword2())) {
				results.add(person);
			}
		}

		return results;

	}

}
