import 'dart:io' show Platform;
import 'package:flutter/foundation.dart' show kIsWeb;

import 'package:flutter/material.dart';
import 'package:flutter_app/models/user.dart';
import 'package:http/http.dart' as http;

import '../utils/user_parsers.dart';

class MyHomePage extends StatefulWidget {
  const MyHomePage({Key? key, required this.title}) : super(key: key);

  // This widget is the home page of your application. It is stateful, meaning
  // that it has a State object (defined below) that contains fields that affect
  // how it looks.

  // This class is the configuration for the state. It holds the values (in this
  // case the title) provided by the parent (in this case the App widget) and
  // used by the build method of the State. Fields in a Widget subclass are
  // always marked "final".

  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> with TickerProviderStateMixin {
  String _getUsersStatus = "";
  List<User>? _users;
  bool _loadingUser = false;
  late AnimationController _controller;

  @override
  void initState() {
    _controller = AnimationController(
      vsync: this,
      duration: const Duration(seconds: 5),
    )..addListener(() {
        setState(() {});
      });
    _controller.repeat();

    Future<void> callGetUsers() async {
      await _getUsers();
    }

    callGetUsers();

    super.initState();
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    // This method is rerun every time setState is called, for instance as done
    // by the _incrementCounter method above.
    //
    // The Flutter framework has been optimized to make rerunning build methods
    // fast, so that you can just rebuild anything that needs updating rather
    // than having to individually change instances of widgets.
    return Scaffold(
      appBar: AppBar(
        // Here we take the value from the MyHomePage object that was created by
        // the App.build method, and use it to set our appbar title.
        title: Text(widget.title),
      ),
      body: Column(
        children: _loadingUser
            ? <Widget>[renderLoadingSection()]
            : <Widget>[renderUserSelectionSection()],
      ),
    );
  }

  Widget renderUserSelectionSection() {
    if (_getUsersStatus != "OK") {
      return Center(
        child: Column(
          children: <Widget>[
            Text('Ha fallado la solicitud de obtener usuarios',
                style: Theme.of(context).textTheme.headline5),
            ElevatedButton(
                onPressed: _getUsers, child: const Text('Reintentar')),
          ],
        ),
      );
    }

    return Container(
        padding: const EdgeInsets.all(8),
        child: Column(
          children: [
            Row(
              children: [
                Text('Seleccionar usuario',
                    style: Theme.of(context).textTheme.headline5),
              ],
            ),
            ListView.separated(
              shrinkWrap: true,
              padding: const EdgeInsets.all(8),
              itemCount: _users?.length ?? 0,
              itemBuilder: (BuildContext context, int index) {
                return Row(
                  children: [
                    Text(_users![index].name),
                    Container(
                      margin: const EdgeInsets.only(left: 10.0),
                      child: Text(
                        '(${_users![index].roleName})',
                      ),
                    )
                  ],
                );
              },
              separatorBuilder: (BuildContext context, int index) =>
                  const Divider(),
            )
          ],
        ));
  }

  Widget renderLoadingSection() => Center(
        child: CircularProgressIndicator(
          value: _controller.value,
          semanticsLabel: 'Circular progress indicator',
        ),
      );

  Future<void> _getUsers() async {
    setState(() {
      _getUsersStatus = "";
      _loadingUser = true;
      _users = null;
    });

    try {
      String urlString = 'http://localhost:8011/Users';

      // TODO: Buscar o crear una librería que simplifique esto
      if (!kIsWeb && (Platform.isAndroid || Platform.isMacOS)) {
        urlString = 'http://10.0.2.2:8011/Users';
      }

      var url = Uri.parse(urlString);
      var response = await http.get(url);

      setState(() {
        if (response.statusCode == 200) {
          _users = parseJsonToUserList(response.body);
        }

        _getUsersStatus = response.statusCode == 200 ? "OK" : "FAIL";
        _loadingUser = false;
      });
    } catch (e) {
      setState(() {
        _getUsersStatus = "FAIL";
        _loadingUser = false;
      });
    }
  }
}
