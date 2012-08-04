Feature: Session Resume
	In order to moderate a session
	As a moderator
	I want to resume an existing session

@web @db
Scenario: Display accounts
	Given I have browsed to the home page
	When I click "Log in with Stack Exchange"
	Then my 4 accounts are listed

@web @db
Scenario: View list of available sessions
	Given I have an active session
	And I have browsed to the home page
	And I have clicked "Log in with Stack Exchange"
	When I click the Stack Overflow account
	Then I get redirected to the session list page
	And 1 session is available to resume

@web @db
Scenario: Select an existing session
	Given I have an active session
	And I have browsed to the home page
	And I have clicked "Log in with Stack Exchange"
	And I have clicked the Stack Overflow account
	When I choose the existing session to resume
	Then I am redirected to the session index page
