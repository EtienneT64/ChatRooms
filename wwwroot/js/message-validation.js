function filterOutTags(inputString) {
	const tagsRegex = /<br\s*\/?>|<\/br>|<p\s*\/?>|<\/p>/gi;

	// Replace matching patterns with an empty string
	const filteredString = inputString.replace(tagsRegex, '');

	return filteredString;
}