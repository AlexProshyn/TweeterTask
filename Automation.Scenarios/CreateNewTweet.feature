Feature: CreateNewTweet

@NewTweet
Scenario: Text_tweet_is_created_via_top_bar_button
	Given Navigate to the start page
	Then Start page is opened
	When Login from Start page using 'validLoginAndPassword' credentials
	Then News feed page is opened
	Then Top bar menu is shown
	When Number of tweets is saved
	When Navigate to the new tweet modal from top bar
	Then New tweet modal is opened
	When Post text tweet from top bar
	When Alert message is disappeared
	When Page is refreshed
	Then New text tweet is appeared in news feed
	Then Number of tweets is increased on news feed page
	When Navigate to the Profile page
	Then Profile page is opened
	Then New text tweet is appeared on profile page
	Then Number of tweets is increased on profile page

@NewTweet
Scenario: Image_tweet_is_created_via_top_bar_button
	Given Navigate to the start page
	Then Start page is opened
	When Login from Start page using 'validLoginAndPassword' credentials
	Then News feed page is opened
	Then Top bar menu is shown
	When Number of tweets is saved
	When Navigate to the new tweet modal from top bar
	Then New tweet modal is opened
	When Post image tweet from top bar
	When Alert message is disappeared
	When Page is refreshed
	Then New image tweet is appeared in news feed
	Then Number of tweets is increased on news feed page
	When Navigate to the Profile page
	Then Profile page is opened
	Then New image tweet is appeared on profile page
	Then Number of tweets is increased on profile page

@NewTweet
Scenario: Text_tweet_is_created_via_news_feed
	Given Navigate to the start page
	Then Start page is opened
	When Login from Start page using 'validLoginAndPassword' credentials
	Then News feed page is opened
	Then Top bar menu is shown
	When Number of tweets is saved
	When Post text tweet from news feed
	When Page is refreshed
	Then New text tweet is appeared in news feed
	Then Number of tweets is increased on news feed page
	When Navigate to the Profile page
	Then Profile page is opened
	Then New text tweet is appeared on profile page
	Then Number of tweets is increased on profile page

@NewTweet
Scenario: Image_tweet_is_created_via_news_feed
	Given Navigate to the start page
	Then Start page is opened
	When Login from Start page using 'validLoginAndPassword' credentials
	Then News feed page is opened
	Then Top bar menu is shown
	When Number of tweets is saved
	When Post image tweet from news feed
	When Page is refreshed
	Then New image tweet is appeared in news feed
	Then Number of tweets is increased on news feed page
	When Navigate to the Profile page
	Then Profile page is opened
	Then New image tweet is appeared on profile page
	Then Number of tweets is increased on profile page
