Feature: Login3
	Check if login functionality works


@mytag
Scenario: Login3 user as Administrator
	Given I navigate to application
	And I click the Login link
	And I enter username and password
		| UserName | Password |
		| admin    | password |
	When I click login
	Then I should see user logged in to the application

	@mytag
Scenario Outline: Login3 user as AdministratorAA
	Given I navigate to application <test>
	And I click the Login link
	And I enter username and password
		| UserName | Password |
		| <admin>    | <password2> |
	When I click login
	Then I should see user logged in to the application

	Examples:
  | admin | password2 | test  |
  | admin | password  | test1 |
  | admin | password1 |test2 |
  | Admin | password  |test3 |
  | admin | Password |test4 |
    