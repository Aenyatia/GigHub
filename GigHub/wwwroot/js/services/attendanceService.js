var AttendanceService = function () {
	const createAttendance = function (gigId, done, fail) {
		$.ajax({
			url: "/api/attendances",
			method: "POST",
			contentType: "application/json",
			data: JSON.stringify({
				gigId: gigId
			})
		})
			.done(done)
			.fail(fail);
	};

	const deleteAttendance = function (gigId, done, fail) {
		$.ajax({
			url: `/api/attendances/${gigId}`,
			method: "DELETE"
		})
			.done(done)
			.fail(fail);
	};

	return {
		createAttendance: createAttendance,
		deleteAttendance: deleteAttendance
	};
}();
