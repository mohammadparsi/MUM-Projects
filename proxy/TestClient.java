package rideshare.framework.proxy;

import java.util.List;

public class TestClient {
public static void main(String[] args) {
	CacheProxy<Person> proxy = new CacheProxy<Person>();
	SearchInput input = new SearchInput("Mohammad","Iran");
	
	//first request
	List<Person> people = proxy.request(input);
	
	for (Person person : people) {
		System.out.println(person.getName()+" from "+person.getCountry());
	}
	
	//second request
	people = proxy.request(input);
	
	for (Person person : people) {
		System.out.println(person.getName()+" from "+person.getCountry());
	}
	//third request
	SearchInput input2 = new SearchInput("Lwan","Eritrea");
	people = proxy.request(input2);
	
	for (Person person : people) {
		System.out.println(person.getName()+" from "+person.getCountry());
	}

}
}
