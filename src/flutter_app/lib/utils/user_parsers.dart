// A function that converts a response body into a List<Photo>.
import 'dart:convert';

import 'package:flutter_app/models/user.dart';

List<User> parseJsonToUserList(String responseBody) {
  final parsed = jsonDecode(responseBody).cast<Map<String, dynamic>>();

  return parsed.map<User>((json) => User.fromJson(json)).toList();
}
