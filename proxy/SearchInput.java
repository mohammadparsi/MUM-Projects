package rideshare.framework.proxy;

public class SearchInput {
	
	String searchKeyword1;
	String searchKeyword2;
	
	public SearchInput(String searchKeyword1, String searchKeyword2) {
		this.searchKeyword1 = searchKeyword1;
		this.searchKeyword2 = searchKeyword2;
	}
	
	
	public String getSearchKeyword1() {
		return searchKeyword1;
	}


	public void setSearchKeyword1(String searchKeyword1) {
		this.searchKeyword1 = searchKeyword1;
	}


	public String getSearchKeyword2() {
		return searchKeyword2;
	}


	public void setSearchKeyword2(String searchKeyword2) {
		this.searchKeyword2 = searchKeyword2;
	}


	public String concatinateSearchKeywords() {
		return (searchKeyword2 + searchKeyword2).toString();
	}
}
