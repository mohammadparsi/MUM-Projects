package rideshare.framework.proxy.observer;

import java.util.ArrayList;
import java.util.List;

import rideshare.framework.proxy.Person;
import rideshare.framework.proxy.PersonDao;
import rideshare.framework.proxy.SearchInput;

public class PersonDaoResource extends ResourceSubjectImpl<PersonDao> {
	
	
	public  List<Person> getAllPeople() {
		return people;
	}

	
	
	private static PersonDaoResource singletonInstance;
	 List<Person> people = new ArrayList<Person>();
    // SingletonExample prevents any other class from instantiating
    private PersonDaoResource() {
    	people.add(new Person("Mohammad","Iran"));
		people.add(new Person("Tesfay","Ethiopia"));
		people.add(new Person("Lwan","Eritrea"));
		people.add(new Person("ChanPiseth","Vietnam"));
		
    }
    // Providing Global point of access
    public static PersonDaoResource getSingletonInstance() {
        if (null == singletonInstance) {
            singletonInstance = new PersonDaoResource();
            
        }
        
        return singletonInstance;
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
	
	public void remove(Person person) {

		 for (int i = 0; i < people.size(); i++) {
			Person p = people.get(i);
			if(p.getName().equals(person.getName()))
			{
				people.remove(i);
			}
		 }
		 
		 
		//setRemovedEntity(person);

	}
	


}
