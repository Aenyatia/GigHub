var GigDetailsController = function (followingService) {
	var followButton;

	var fail = function () {
		alert("Something failed!");
	};

	var done = function() {
		var text = (followButton.text() === "Follow") ? "Following" : "Follow";

		followButton.text(text);
	};

	var toggleFollow = function (e) {
		followButton = $(e.target);
		var artistId = followButton.attr("data-user-id");

		if (followButton.text() === "Follow")
			followingService.createFollow(artistId, done, fail);
		else
			followingService.deleteFollow(artistId, done, fail);
	};

	var init = function (container) {
		//$(container).on("click", ".js-toggle-follow", toggleFollow);
		$(".js-toggle-follow").click(toggleFollow);
	};

	return {
		init: init
	};

}(FollowingService);
