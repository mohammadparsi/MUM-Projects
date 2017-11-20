package rideshare.framework.proxy.observer;

import java.util.List;

import rideshare.framework.proxy.Person;
import rideshare.framework.proxy.SearchInput;

public class TestClient2 {
	public static void main(String[] args) {
		CachingProxy<Person> proxy = new CachingProxy(PersonDaoResource.getSingletonInstance());
		SearchInput input = new SearchInput("Mohammad", "Iran");

		// first request
		List<Person> people = proxy.request(input);

		printPeople(people);

		// second request
		people = proxy.request(input);

		printPeople(people);

		// third request
		SearchInput input2 = new SearchInput("Lwan", "Eritrea");
		people = proxy.request(input2);

		printPeople(people);

		PersonDaoResource dao =PersonDaoResource.getSingletonInstance();
		System.out.println("removing mohammad from Database");
		dao.remove(new Person("Mohammad", "Iran"));
//		System.out.println("Mohammd removed from Database");

		//fourth request after removing mohammad from database
		//requesting mohammad from 
		System.out.println("requesting mohammad from proxy cache");
		people = proxy.request(input);
		
		printPeople(people);

	}

	private static void printPeople(List<Person> people) {
		if (people.size() == 0) {
			System.out.println("No one found.");
		} else {
			for (Person person : people) {
				System.out.println(person.getName() + " from " + person.getCountry());
			}
		}

	}
}
