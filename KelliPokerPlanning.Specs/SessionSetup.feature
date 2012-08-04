Feature: Session Setup
	In order to moderate a session
	As a moderator
	I want to start a new session

@web @db
Scenario: Select an account with no sessions
	Given I have browsed to the home page
	And I have clicked "Log in with Stack Exchange"
	When I click the Stack Overflow account
	Then I get redirected to the session setup page

@web @db
Scenario: Setup a session
	Given I have browsed to the home page
	And I have clicked "Log in with Stack Exchange"
	And I have clicked the Stack Overflow account
	And I have entered fibonacci values
	And I have unchecked question
	And I have unchecked infinity
	When I submit the form
	Then I am redirected to the session index page