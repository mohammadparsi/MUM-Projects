package rideshare.framework.proxy;

public class Person {
@Override
	public String toString() {
		// TODO Auto-generated method stub
		return name;
	}

@Override
	public boolean equals(Object obj) {
		// TODO Auto-generated method stub
		Person person = (Person)obj;
		if (person.getCountry().equals(country)&& person.getName().equals(name)) {
			return true;
		}
		return false;
	}

public String getName() {
		return name;
	}

	public String getCountry() {
		return country;
	}
	
	



private String name;
private String country;

public Person(String name, String country) {
	
	this.name = name;
	this.country=country;
}




}
